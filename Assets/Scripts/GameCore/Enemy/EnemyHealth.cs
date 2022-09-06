using System;
using GameCore.CommonLogic;
using UnityEngine;

namespace GameCore.Enemy {
    public sealed class EnemyHealth : MonoBehaviour, IHealth {
        [SerializeField] EnemyDamageEffect DamageEffect;
        
        public event Action HealthChanged;

        public float CurrentHP { get; set; }

        public float MaxHP { get; set; }

        public void Init(float health) {
            MaxHP = health;
            CurrentHP = health;
        }
        
        public void TakeDamage(float damage) {
            if ( CurrentHP <= 0f ) {
                return;
            }
            
            CurrentHP -= damage;
            HealthChanged?.Invoke();
            DamageEffect.Play();
        }
    }
}