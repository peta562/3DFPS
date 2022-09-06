using System;
using Data;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows {
    public abstract class BaseWindow : MonoBehaviour {
        [SerializeField] Button CloseButton;

        ISaveDataHandler _saveDataHandler;
        IPauseService _pauseService;

        protected PlayerSaveData PlayerSaveData => _saveDataHandler.SaveData.PlayerSaveData;

        public void Init(ISaveDataHandler saveDataHandler, IPauseService pauseService) {
            _saveDataHandler = saveDataHandler;
            _pauseService = pauseService;
        }
        
        void Awake() {
            CloseButton.onClick.AddListener(Hide);
        }

        void Start() {
            InitInternal();
            SubscribeUpdates();
            _pauseService.SetPause(true);
        }

        void OnDestroy() {
            CloseButton.onClick.RemoveListener(Hide);
            UnsubscribeUpdates();
        }

        void Hide() {
            _pauseService.SetPause(false);
            Destroy(gameObject);
        }

        protected virtual void InitInternal(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void UnsubscribeUpdates(){}
    }
}