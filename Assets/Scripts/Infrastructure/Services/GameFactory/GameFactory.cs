using GameCore.CommonLogic;
using GameCore.CommonLogic.EnemySpawners;
using GameCore.Enemy;
using GameCore.Loot;
using GameCore.Weapons;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Configs;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.WindowService;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.GameFactory {
    public sealed class GameFactory : IGameFactory {
        readonly ISaveDataHandler _saveDataHandler;
        readonly IAssetProvider _assetProvider;
        readonly IConfigProvider _configProvider;
        readonly IWindowManager _windowManager;
        readonly IPauseService _pauseService;

        GameObject _player;

        public GameFactory(ISaveDataHandler saveDataHandler, IAssetProvider assetProvider,
            IConfigProvider configProvider, IWindowManager windowManager, IPauseService pauseService) {
            _saveDataHandler = saveDataHandler;
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _windowManager = windowManager;
            _pauseService = pauseService;
        }

        public GameObject CreatePlayer(Vector3 position) {
            _player = InstantiateRegistered(AssetPath.PlayerPath, position);

            return _player;
        }

        public GameObject CreateHUD() {
            var hud = InstantiateRegistered(AssetPath.HudPath);

            hud.GetComponentInChildren<LootUI>().Init(_saveDataHandler.SaveData.PlayerSaveData.PlayerLootData);

            foreach (var showWindowButton in hud.GetComponentsInChildren<ShowWindowButton>()) {
                showWindowButton.Init(_windowManager);
            }
            return hud;
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent) {
            var enemyConfig = _configProvider.GetEnemyConfig(enemyTypeId);

            var enemyObject = Object.Instantiate(enemyConfig.Prefab, parent.position, Quaternion.identity, parent);
            Register(enemyObject);

            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.Init(_player.transform, enemyConfig.Speed, enemyConfig.StoppingDistance, enemyConfig.AttackCooldown,
                enemyConfig.AttackDistance, enemyConfig.Damage, enemyConfig.Health, enemyConfig.MinLoot, enemyConfig.MaxLoot, CreateLoot);

            return enemyObject;
        }

        public GameObject CreateWeapon(WeaponTypeId weaponTypeId, Transform parent) {
            var weaponConfig = _configProvider.GetWeaponConfig(weaponTypeId);

            var weaponObject = Object.Instantiate(weaponConfig.Prefab, parent.position, Quaternion.identity, parent);
            Register(weaponObject);

            var weapon = weaponObject.GetComponent<Weapon>();
            weapon.Init(weaponConfig.Damage, weaponConfig.BulletSpeed);

            return weaponObject;
        }

        public LootPiece CreateLoot(int lootMin, int lootMax) {
            var lootPiece = InstantiateRegistered(AssetPath.LootPath).GetComponent<LootPiece>();

            var loot = new Loot {
                Value = Random.Range(lootMin, lootMax)
            };
            
            lootPiece.Init(_saveDataHandler.SaveData.PlayerSaveData.PlayerLootData, loot);

            return lootPiece;
        }

        public void CreateSpawner(Vector3 position, float spawnTime) {
            var spawner = InstantiateRegistered(AssetPath.EnemySpawnPointPath, position).GetComponent<SpawnPoint>();

            spawner.Init(spawnTime, CreateEnemy);
        }

        void Register(GameObject gameObject) {
            RegisterSaveWriters(gameObject);
            RegisterSaveReaders(gameObject);
            RegisterPauseHandlers(gameObject);
        }

        GameObject InstantiateRegistered(string prefabPath, Vector3 at) {
            var gameObject = _assetProvider.Instantiate(path: prefabPath, at: at);

            Register(gameObject);
            return gameObject;
        }

        GameObject InstantiateRegistered(string prefabPath) {
            var gameObject = _assetProvider.Instantiate(path: prefabPath);

            Register(gameObject);
            return gameObject;
        }

        void RegisterPauseHandlers(GameObject gameObject) {
            foreach (var pauseHandler in gameObject.GetComponentsInChildren<IPauseHandler>()) {
                _pauseService.Register(pauseHandler);
            }
        }

        void RegisterSaveWriters(GameObject gameObject) {
            foreach (var saveWriter in gameObject.GetComponentsInChildren<ISaveWriter>()) {
                _saveDataHandler.RegisterSaveWriter(saveWriter);
            }
        }

        void RegisterSaveReaders(GameObject gameObject) {
            foreach (var saveReader in gameObject.GetComponentsInChildren<ISaveReader>()) {
                _saveDataHandler.RegisterSaveReader(saveReader);
            }
        }
    }
}