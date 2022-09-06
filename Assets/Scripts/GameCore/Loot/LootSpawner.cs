using System;
using Data;
using GameCore.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCore.Loot {
    public sealed class LootSpawner : MonoBehaviour {
        [SerializeField] EnemyDeath EnemyDeath;

        Func<int, int, LootPiece> _lootSpawnMethod;
        int _lootMax;
        int _lootMin;

        public void Init(int minLoot, int maxLoot, Func<int, int, LootPiece> lootSpawnMethod) {
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

        void SpawnLoot() {
            var lootPiece = _lootSpawnMethod.Invoke(_lootMin, _lootMax);
            lootPiece.transform.position = transform.position;
        }
    }
}