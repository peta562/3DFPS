using System;
using System.Threading.Tasks;
using GameCore.Loot;
using Infrastructure.Services.PauseService;
using UnityEngine;

namespace GameCore.Enemy {
    public sealed class Enemy : MonoBehaviour, IPauseHandler {
        [Header("Attack Behaviour")] [SerializeField]
        EnemyAttack EnemyAttack;

        [Header("Move Behaviour")] [SerializeField]
        EnemyMove EnemyMove;

        [Header("Health Behaviour")] [SerializeField]
        EnemyHealth EnemyHealth;

        [Header("Death Behaviour")] [SerializeField]
        EnemyDeath EnemyDeath;

        [Header("Loot Behaviour")] [SerializeField]
        LootSpawner LootSpawner;

        bool _isPaused;

        public void Init(Transform destinationTransform, float speed, float stoppingDistance, float attackCooldown,
            float attackDistance, float damage, float health, int minLoot, int maxLoot,
            Func<int, int, Task<LootPiece>> lootSpawnMethod) {
            EnemyMove.Init(destinationTransform, speed, stoppingDistance);
            EnemyAttack.Init(destinationTransform, attackCooldown, attackDistance, damage);
            EnemyHealth.Init(health);
            EnemyDeath.Init(EnemyHealth);
            LootSpawner.Init(minLoot, maxLoot, lootSpawnMethod);
        }

        void Update() {
            if ( _isPaused ) {
                return;
            }

            EnemyMove.Move();
            EnemyAttack.Attack();
        }

        public void OnPauseChanged(bool isPaused) {
            _isPaused = isPaused;
        }
    }
}