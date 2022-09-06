using GameCore.CommonLogic;
using GameCore.Loading;
using Infrastructure.Services.Configs;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.UIFactory;
using UI;
using UnityEngine;

namespace Infrastructure.StateMachine {
    public sealed class LoadLevelState : IPayloadedState<SceneName> {
        const string InitialPointTag = "InitialPoint";
        
        readonly GameStateMachine _stateMachine;
        readonly SceneLoader _sceneLoader;
        readonly LoadingScreen _loadingScreen;
        readonly ISaveDataHandler _saveDataHandler;
        readonly IGameFactory _gameFactory;
        readonly IConfigProvider _configProvider;
        readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            ISaveDataHandler saveDataHandler, IGameFactory gameFactory, IConfigProvider configProvider, IUIFactory uiFactory) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _saveDataHandler = saveDataHandler;
            _gameFactory = gameFactory;
            _configProvider = configProvider;
            _uiFactory = uiFactory;
        }

        public void Enter(SceneName sceneName) {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() {
            _loadingScreen.Hide();
        }

        void OnLoaded() {
            InitUIRoot();
            InitLevel();
            InformSaveReaders();
            
            _stateMachine.Enter<GameLoopState>();
        }

        void InitUIRoot() {
            _uiFactory.CreateUIRoot();
        }

        void InitLevel() {
            var player = _gameFactory.CreatePlayer(GameObject.FindWithTag(InitialPointTag));

            var sceneName = _sceneLoader.CurrentSceneName;
            var levelConfig = _configProvider.GetLevelConfig(sceneName);
            foreach (var enemySpawnerConfig in levelConfig.EnemySpawnerConfigs) {
                _gameFactory.CreateSpawner(enemySpawnerConfig.Position);
            }

            var hud =  _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>().Init(player.GetComponent<IHealth>());
        }
        
        void InformSaveReaders() {
            foreach (var saveReader in _saveDataHandler.SaveReaders) {
                saveReader.LoadSave(_saveDataHandler.SaveData);
            }   
        }
    }
}