using System.Threading.Tasks;
using GameCore.CommonLogic;
using GameCore.CommonLogic.EnemySpawners;
using GameCore.Enemy;
using GameCore.Loot;
using GameCore.Player;
using GameCore.Weapons;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Configs;
using Infrastructure.Services.Input;
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
        readonly IInputService _inputService;

        GameObject _playerObject;

        public GameFactory(ISaveDataHandler saveDataHandler, IAssetProvider assetProvider,
            IConfigProvider configProvider, IWindowManager windowManager, IPauseService pauseService,
            IInputService inputService) {
            _saveDataHandler = saveDataHandler;
            _assetProvider = assetProvider;
            _configProvider = configProvider;
            _windowManager = windowManager;
            _pauseService = pauseService;
            _inputService = inputService;
        }

        public async Task WarmUp() {
            await _assetProvider.Load<GameObject>(AssetAddress.Loot);
            await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawnPoint);
        }

        public async Task<GameObject> CreatePlayer(Vector3 position) {
            _playerObject = await InstantiateRegisteredAsync(AssetAddress.Player, position);

            var playerConfig = _configProvider.GetPlayerConfig();

            var player = _playerObject.GetComponent<Player>();
            player.Init(_inputService, CreateWeapon, playerConfig.AttackCooldown, playerConfig.MovementSpeed,
                playerConfig.MouseSensitivity);

            return _playerObject;
        }

        public async Task<GameObject> CreateHUD() {
            var hud = await InstantiateRegisteredAsync(AssetAddress.Hud);

            hud.GetComponentInChildren<LootUI>().Init(_saveDataHandler.SaveData.PlayerSaveData.PlayerLootData);

            foreach (var showWindowButton in hud.GetComponentsInChildren<ShowWindowButton>()) {
                showWindowButton.Init(_windowManager);
            }

            return hud;
        }

        public async Task<MainMenuUI> CreateMainMenuUI() {
            var mainMenuObject = await InstantiateRegisteredAsync(AssetAddress.MainMenuUI);
            return mainMenuObject.GetComponent<MainMenuUI>();
        }

        public void Cleanup() {
            _saveDataHandler.SaveReaders.Clear();
            _saveDataHandler.SaveWriters.Clear();

            _assetProvider.CleanUp();
        }

        public async Task<GameObject> CreateEnemy(EnemyTypeId enemyTypeId, Transform parent) {
            var enemyConfig = _configProvider.GetEnemyConfig(enemyTypeId);

            var prefab = await _assetProvider.Load<GameObject>(enemyConfig.PrefabReference);

            var enemyObject = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            Register(enemyObject);

            var enemy = enemyObject.GetComponent<Enemy>();
            enemy.Init(_playerObject.transform, enemyConfig.Speed, enemyConfig.StoppingDistance,
                enemyConfig.AttackCooldown,
                enemyConfig.AttackDistance, enemyConfig.Damage, enemyConfig.Health, enemyConfig.MinLoot,
                enemyConfig.MaxLoot, CreateLoot);

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

        public async Task<LootPiece> CreateLoot(int lootMin, int lootMax) {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.Loot);

            var lootPiece = InstantiateRegistered(prefab).GetComponent<LootPiece>();

            var loot = new Loot {
                Value = Random.Range(lootMin, lootMax)
            };

            lootPiece.Init(_saveDataHandler.SaveData.PlayerSaveData.PlayerLootData, loot);

            return lootPiece;
        }

        public async Task CreateSpawner(Vector3 position, float spawnTime) {
            var prefab = await _assetProvider.Load<GameObject>(AssetAddress.EnemySpawnPoint);
            var spawner = InstantiateRegistered(prefab, position).GetComponent<SpawnPoint>();

            spawner.Init(spawnTime, CreateEnemy);
        }

        void Register(GameObject gameObject) {
            RegisterSaveWriters(gameObject);
            RegisterSaveReaders(gameObject);
            RegisterPauseHandlers(gameObject);
        }

        async Task<GameObject> InstantiateRegisteredAsync(string address, Vector3 at) {
            var gameObject = await _assetProvider.Instantiate(address, at);

            Register(gameObject);
            return gameObject;
        }

        async Task<GameObject> InstantiateRegisteredAsync(string address) {
            var gameObject = await _assetProvider.Instantiate(address);

            Register(gameObject);
            return gameObject;
        }

        GameObject InstantiateRegistered(GameObject prefab, Vector3 at) {
            var gameObject = Object.Instantiate(prefab, at, Quaternion.identity);

            Register(gameObject);
            return gameObject;
        }

        GameObject InstantiateRegistered(GameObject prefab) {
            var gameObject = Object.Instantiate(prefab);

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