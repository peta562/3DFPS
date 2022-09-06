using Data;
using Infrastructure.Services;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.Input;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;

namespace GameCore.Player {
    public sealed class Player : MonoBehaviour, ISaveReader, ISaveWriter, IPauseHandler {
        [Header("Settings")] 
        [SerializeField] PlayerAttack PlayerAttack;
        [SerializeField] Transform WeaponSlot;
        [SerializeField] float AttackCooldown = 3f;

        [Header("Settings")] 
        [SerializeField] PlayerMove PlayerMove;
        [SerializeField] float MovementSpeed = 15f;

        [Header("Camera Settings")] 
        [SerializeField] PlayerCamera PlayerCamera;
        [SerializeField] Camera Camera;
        [SerializeField] float MouseSensitivity = 100f;

        [Header("Health Settings")] 
        [SerializeField] PlayerHealth PlayerHealth;

        [Header("Death")] 
        [SerializeField] PlayerDeath PlayerDeath;
        
        PlayerSaveData _playerSaveData;
        
        IInputService _inputService;
        IGameFactory _gameFactory;
        
        bool _isDead;
        bool _isPaused;
        
        void Awake() {
            _inputService = ServiceLocator.Services.Single<IInputService>();
            _gameFactory = ServiceLocator.Services.Single<IGameFactory>();
        }

        void Update() {
            if ( _isDead || _isPaused ) {
                return;
            }
            
            PlayerMove.TryMove();
            PlayerAttack.TryAttack();
            PlayerCamera.UpdateCamera();
        }

        void OnDead() {
            _isDead = true;
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
            PlayerAttack.Init(_inputService, _gameFactory, _playerSaveData.PlayerWeaponData, WeaponSlot,
                AttackCooldown);
            PlayerMove.Init(_inputService, _playerSaveData.PlayerPositionData, MovementSpeed);
            PlayerCamera.Init(_inputService, Camera, MouseSensitivity);
            PlayerHealth.Init(_playerSaveData.PlayerHealthData);
            PlayerDeath.Init(PlayerHealth, OnDead);
        }

        public void OnPauseChanged(bool isPaused) {
            _isPaused = isPaused;
        }
    }
}