using Infrastructure;
using Infrastructure.StateMachine;
using UI;

namespace GameCore.CommonLogic {
    public sealed class MainMenuObserver {
        readonly IGameStateMachine _gameStateMachine;

        public MainMenuObserver(IGameStateMachine gameStateMachine, MainMenuUI mainMenuUI) {
            _gameStateMachine = gameStateMachine;
            mainMenuUI.PlayButtonClicked += PlayButtonClicked;
        }

        void PlayButtonClicked() {
            EnterLevel();
        }
        
        void EnterLevel() {
            _gameStateMachine.Enter<LoadLevelState, SceneName>(SceneName.Level);
        }
    }
}