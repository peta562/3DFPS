﻿using System;
using Data;
using GameCore.CommonLogic;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.Input;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;

namespace GameCore.Player {
    public sealed class Player : MonoBehaviour, ISaveReader, ISaveWriter, IPauseHandler {
        [Header("Settings")] [SerializeField] PlayerAttack PlayerAttack;
        [SerializeField] Transform WeaponSlot;

        [Header("Settings")] [SerializeField] PlayerMove PlayerMove;

        [Header("Camera Settings")] [SerializeField]
        PlayerCamera PlayerCamera;

        [SerializeField] Camera Camera;

        [Header("Health Settings")] [SerializeField]
        PlayerHealth PlayerHealth;

        [Header("Death")] [SerializeField] PlayerDeath PlayerDeath;

        PlayerSaveData _playerSaveData;

        IInputService _inputService;
        Func<WeaponTypeId, Transform, GameObject> _createWeapon;

        float _attackCooldown;
        float _movementSpeed;
        float _mouseSensitivity;

        bool _isPaused;
        bool _behavioursInited;

        public void Init(IInputService inputService, Func<WeaponTypeId, Transform, GameObject> createWeapon,
            float attackCooldown,
            float movementSpeed, float mouseSensitivity) {
            _inputService = inputService;
            _createWeapon = createWeapon;
            _attackCooldown = attackCooldown;
            _movementSpeed = movementSpeed;
            _mouseSensitivity = mouseSensitivity;
        }

        void Update() {
            if ( _isPaused || !_behavioursInited ) {
                return;
            }

            PlayerMove.TryMove();
            PlayerAttack.TryAttack();
            PlayerCamera.UpdateCamera();
        }

        public void LoadSave(SaveData saveData) {
            _playerSaveData = saveData.PlayerSaveData;

            InitBehaviours();
        }

        public void SaveData(SaveData saveData) {
            PlayerAttack.SaveData(_playerSaveData);
            PlayerMove.SaveData(_playerSaveData);
            PlayerHealth.SaveData(_playerSaveData);

            saveData.PlayerSaveData = _playerSaveData;
        }

        void InitBehaviours() {
            PlayerAttack.Init(_inputService, _createWeapon, _playerSaveData.PlayerWeaponData, WeaponSlot,
                _attackCooldown);
            PlayerMove.Init(_inputService, _playerSaveData.PlayerPositionData, _movementSpeed);
            PlayerCamera.Init(_inputService, Camera, _mouseSensitivity);
            PlayerHealth.Init(_playerSaveData.PlayerHealthData);
            PlayerDeath.Init(PlayerHealth);

            _behavioursInited = true;
        }

        public void OnPauseChanged(bool isPaused) {
            _isPaused = isPaused;
        }
    }
}