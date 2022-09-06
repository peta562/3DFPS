using System;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerDeath : MonoBehaviour {
        PlayerHealth _playerHealth;

        Action _onDead;
        bool _isDead;

        public void Init(PlayerHealth playerHealth, Action onDead) {
            _playerHealth = playerHealth;

            _onDead = onDead;
            _playerHealth.HealthChanged += HealthChanged;
        }

        void OnDestroy() {
            _playerHealth.HealthChanged -= HealthChanged;
        }

        void HealthChanged() {
            if ( !_isDead && _playerHealth.CurrentHP <= 0f ) {
                Die();
            }
        }

        void Die() {
            _isDead = true;
            _onDead?.Invoke();
        }
    }
}