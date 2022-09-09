using System;
using System.Threading.Tasks;
using GameCore.Enemy;
using UnityEngine;

namespace GameCore.Loot {
    public sealed class LootSpawner : MonoBehaviour {
        [SerializeField] EnemyDeath EnemyDeath;

        Func<int, int, Task<LootPiece>> _lootSpawnMethod;
        int _lootMax;
        int _lootMin;

        public void Init(int minLoot, int maxLoot, Func<int, int, Task<LootPiece>> lootSpawnMethod) {
            _lootSpawnMethod = lootSpawnMethod;
            _lootMin = minLoot;
            _lootMax = maxLoot;
        }
        
        void Start() {
            EnemyDeath.OnDead += SpawnLoot;
        }

        void OnDestroy() {
            EnemyDeath.OnDead -= SpawnLoot;
        }

        async void SpawnLoot() {
            var lootPiece = await _lootSpawnMethod.Invoke(_lootMin, _lootMax);
            lootPiece.transform.position = transform.position;
        }
    }
}