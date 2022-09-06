using System;
using GameCore.Loading;
using Infrastructure.Services;
using Infrastructure.Services.SaveLoad;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure {
    public sealed class GameStarter : MonoBehaviour, ICoroutineRunner {
        [SerializeField] LoadingScreen LoadingScreenPrefab;
        
        Game _game;
        
        void Awake() {
            _game = new Game(this, Instantiate(LoadingScreenPrefab));
            _game.StateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }

        void OnApplicationQuit() {
            var saveLoadService = ServiceLocator.Services.Single<ISaveLoadService>();
            saveLoadService.SaveData();
        }
    }
}