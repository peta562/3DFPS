using System;
using System.Collections.Generic;
using GameCore.Loading;
using Infrastructure.Services;
using Infrastructure.Services.Configs;
using Infrastructure.Services.GameFactory;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.SaveLoad;
using Infrastructure.Services.UIFactory;

namespace Infrastructure.StateMachine {
    public sealed class GameStateMachine : IGameStateMachine {
        readonly Dictionary<Type, IExitableState> _states;
        IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, ServiceLocator services) {
            _states = new Dictionary<Type, IExitableState> {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<ISaveDataHandler>(),
                    services.Single<ISaveLoadService>()),
                [typeof(MainMenuState)] = new MainMenuState(),
                [typeof(LoadLevelState)] =
                    new LoadLevelState(this, sceneLoader, loadingScreen, 
                        services.Single<ISaveDataHandler>(),
                        services.Single<IGameFactory>(), 
                        services.Single<IConfigProvider>(),
                        services.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this, 
                    services.Single<IPauseService>()),
            };
        }

        public void Enter<T>() where T : class, IState {
            var state = ChangeState<T>();
            state.Enter();
        }

        public void Enter<T, TP>(TP payload) where T : class, IPayloadedState<TP> {
            var state = ChangeState<T>();
            state.Enter(payload);
        }

        T ChangeState<T>() where T : class, IExitableState {
            _activeState?.Exit();

            var state = GetState<T>();
            _activeState = state;

            return state;
        }

        T GetState<T>() where T : class, IExitableState => _states[typeof(T)] as T;
    }
}