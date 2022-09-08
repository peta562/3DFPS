using System;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerDeath : MonoBehaviour {
        public event Action OnDead;
        
        PlayerHealth _playerHealth;

        bool _isDead;

        public void Init(PlayerHealth playerHealth) {
            _playerHealth = playerHealth;
            
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
            OnDead?.Invoke();
        }
    }
}