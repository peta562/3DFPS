using System;
using Data;
using GameCore.CommonLogic;
using GameCore.Weapons;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.Input;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerAttack : MonoBehaviour {
        WeaponTypeId _weaponTypeId;
        float _attackCooldown;
        Weapon _weapon;
        Transform _weaponSlot;

        Func<WeaponTypeId, Transform, GameObject>  _createWeapon;
        IInputService _inputService;
        
        float _attackCooldownTime;

        public void Init(IInputService inputService, Func<WeaponTypeId, Transform, GameObject> createWeapon, PlayerWeaponData playerWeaponData, Transform weaponSlot, float attackCooldown) {
            _inputService = inputService;
            _createWeapon = createWeapon;
            _weaponTypeId = playerWeaponData.WeaponTypeId;
            _weaponSlot = weaponSlot;
            _attackCooldown = attackCooldown;

            CreateWeapon();
        }

        public void TryAttack() {
            if ( _attackCooldownTime > 0f ) {
                _attackCooldownTime -= Time.deltaTime;
                return;
            }
            
            if ( _inputService.IsAttackButtonUp() ) {
                _weapon.Attack();
                _attackCooldownTime = _attackCooldown;
            }
        }

        public void SaveData(PlayerSaveData playerSaveData) {
            playerSaveData.PlayerWeaponData.WeaponTypeId = _weaponTypeId;
        }

        void CreateWeapon() {
            _weapon = _createWeapon(_weaponTypeId, _weaponSlot).GetComponent<Weapon>();
        }
    }
}