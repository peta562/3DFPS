using Infrastructure.Services;
using Infrastructure.Services.Ads;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.Configs;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.Input;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.UIFactory;
using Infrastructure.Services.WindowService;
using UnityEngine;

namespace Infrastructure.StateMachine {
    public sealed class BootstrapState : IState {
        readonly GameStateMachine _stateMachine;
        readonly ServiceLocator _services;
        readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator services) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter() {
            _sceneLoader.Load(SceneName.EntryPoint, OnLoaded);
        }

        public void Exit() {

        }

        void RegisterServices() {
            RegisterConfigProvider();
            RegisterAdService();
            RegisterAssetProvider();
            
            _services.RegisterSingle<IGameStateMachine>(_stateMachine);

            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IPauseService>(new PauseService());
            _services.RegisterSingle<ISaveDataHandler>(new SaveDataHandler());
            _services.RegisterSingle<ISaveLoadService>(new JsonSaveLoadService(
                _services.Single<ISaveDataHandler>())
            );
            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IConfigProvider>(),
                _services.Single<ISaveDataHandler>(),
                _services.Single<IPauseService>(),
                _services.Single<IAdService>())
            );
            _services.RegisterSingle<IWindowManager>(new WindowManager(
                _services.Single<IUIFactory>())
            );
            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<ISaveDataHandler>(),
                _services.Single<IAssetProvider>(),
                _services.Single<IConfigProvider>(),
                _services.Single<IWindowManager>(),
                _services.Single<IPauseService>(),
                _services.Single<IInputService>())
            );
        }

        void OnLoaded() =>
            _stateMachine.Enter<LoadProgressState>();

        IInputService GetInputService() {
            if ( Application.isEditor )
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        void RegisterConfigProvider() {
            var configProvider = new ConfigProvider();
            configProvider.LoadConfigs();
            _services.RegisterSingle<IConfigProvider>(configProvider);
        }

        void RegisterAssetProvider() {
            var assetProvider = new AssetProvider();
            assetProvider.Init();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
        }

        void RegisterAdService() {
            var adService = new UnityAdService();
            adService.Init();
            _services.RegisterSingle<IAdService>(adService);
        }
    }
}