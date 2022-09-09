using System;
using System.Threading.Tasks;
using Data;
using GameCore.Enemy;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.CommonLogic.EnemySpawners {
    public sealed class SpawnPoint : MonoBehaviour, ISaveWriter, IPauseHandler {
        Func<EnemyTypeId, Transform, Task<GameObject>> _enemySpawnMethod;

        int _killedEnemies;
        EnemyDeath _enemyDeath;

        bool _isPaused;
        
        float _spawnTime;
        bool _canSpawn;
        float _timer;

        public void Init(float spawnTime, Func<EnemyTypeId, Transform, Task<GameObject>> enemySpawnMethod) {
            _enemySpawnMethod = enemySpawnMethod;

            _spawnTime = spawnTime;
            _canSpawn = true;
        }

        void Update() {
            if ( !_canSpawn || _isPaused ) {
                return;
            }

            _timer -= Time.deltaTime;

            if ( _timer <= 0f ) {
                Spawn();
                _timer += _spawnTime;
            }
        }

        public void SaveData(SaveData saveData) {
            saveData.PlayerSaveData.PlayerKillData.KilledEnemies += _killedEnemies;
        }

        async void Spawn() {
            var randomEnemyTypeId = GetRandomEnemyTypeId();
            var enemy = await _enemySpawnMethod.Invoke(randomEnemyTypeId, transform);

            if ( enemy == null ) {
                Debug.LogError("Enemy is null");
                return;
            }

            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.OnDead += EnemyKill;
        }

        EnemyTypeId GetRandomEnemyTypeId() =>
            (EnemyTypeId) Random.Range(0, Enum.GetValues(typeof(EnemyTypeId)).Length);

        void EnemyKill() {
            if ( _enemyDeath != null ) {
                _enemyDeath.OnDead -= EnemyKill;
            }

            _killedEnemies += 1;
        }

        public void OnPauseChanged(bool isPaused) {
            _isPaused = isPaused;
        }
    }
}