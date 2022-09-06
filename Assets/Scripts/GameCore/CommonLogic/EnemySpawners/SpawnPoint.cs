using System;
using Data;
using GameCore.Enemy;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.CommonLogic.EnemySpawners {
    public sealed class SpawnPoint : MonoBehaviour, ISaveWriter, IPauseHandler {
        Func<EnemyTypeId, Transform, GameObject> _enemySpawnMethod;
        
        int _killedEnemies;
        EnemyDeath _enemyDeath;
        
        bool _isPaused;

        public void Init(Func<EnemyTypeId, Transform, GameObject> enemySpawnMethod) {
            _enemySpawnMethod = enemySpawnMethod;

            Spawn();
        }
        
        public void SaveData(SaveData saveData) {
            saveData.PlayerSaveData.PlayerKillData.KilledEnemies += _killedEnemies;
        }

        void Spawn() {
            var randomEnemyTypeId = GetRandomEnemyTypeId();
            var enemy = _enemySpawnMethod?.Invoke(randomEnemyTypeId, transform);

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