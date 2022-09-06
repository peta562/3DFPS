using System;
using UnityEngine;

namespace GameCore.Enemy {
    public sealed class EnemyDeath : MonoBehaviour {
        public event Action OnDead;
        
        EnemyHealth _enemyHealth;

        bool _isDead;
        
        public void Init(EnemyHealth playerHealth) {
            _enemyHealth = playerHealth;

            _enemyHealth.HealthChanged += HealthChanged;
        }

        void OnDestroy() {
            _enemyHealth.HealthChanged -= HealthChanged;
        }

        void HealthChanged() {
            if ( !_isDead && _enemyHealth.CurrentHP <= 0f ) {
                Die();
            }
        }

        void Die() {
            _isDead = true;
            OnDead?.Invoke();
            Destroy(gameObject);
        }
    }
}