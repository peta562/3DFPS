using System.Threading.Tasks;
using GameCore.CommonLogic;
using GameCore.Loading;
using Infrastructure.Services.GameFactory;

namespace Infrastructure.StateMachine {
    public sealed class MainMenuState : IPayloadedState<SceneName> {
        readonly GameStateMachine _stateMachine;
        readonly SceneLoader _sceneLoader;
        readonly LoadingScreen _loadingScreen;
        readonly IGameFactory _gameFactory;

        public MainMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen,
            IGameFactory gameFactory) {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(SceneName sceneName) {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() {
        }

        async void OnLoaded() {
            _loadingScreen.Hide();
            await InitUI();
        }

        async Task InitUI() {
            var mainMenuUI = await _gameFactory.CreateMainMenuUI();

            new MainMenuObserver(_stateMachine, mainMenuUI);
        }
    }
}