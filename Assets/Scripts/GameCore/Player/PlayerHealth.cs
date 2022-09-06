using System;
using Data;
using GameCore.CommonLogic;
using UI;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerHealth : MonoBehaviour, IHealth {
        [SerializeField] UIDamageEffect DamageEffect;
        
        public event Action HealthChanged;

        PlayerHealthData _playerHealthData;

        public float CurrentHP {
            get => _playerHealthData.CurrentHP;
            set {
                if ( _playerHealthData.CurrentHP != value ) {
                    _playerHealthData.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float MaxHP {
            get => _playerHealthData.MaxHP;
            set => _playerHealthData.MaxHP = value;
        }

        public void Init(PlayerHealthData playerHealthData) {
            _playerHealthData = playerHealthData;
            HealthChanged?.Invoke();
        }

        public void TakeDamage(float damage) {
            if ( CurrentHP <= 0f ) {
                return;
            }
            
            CurrentHP -= damage;
            DamageEffect.Play();
        }

        public void SaveData(PlayerSaveData playerSaveData) {
            playerSaveData.PlayerHealthData.CurrentHP = CurrentHP;
            playerSaveData.PlayerHealthData.MaxHP = MaxHP;
        }
    }
}