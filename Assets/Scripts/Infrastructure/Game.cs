using GameCore.Loading;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure {
    public sealed class Game {
        public GameStateMachine StateMachine { get; }

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen) {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, ServiceLocator.Services);
        }
    }
}