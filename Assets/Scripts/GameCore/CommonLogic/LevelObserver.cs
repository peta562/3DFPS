using GameCore.Player;
using Infrastructure;
using Infrastructure.StateMachine;

namespace GameCore.CommonLogic {
    public sealed class LevelObserver {
        IGameStateMachine _gameStateMachine;
        PlayerDeath _playerDeath;

        public void Init(IGameStateMachine gameStateMachine, PlayerDeath playerDeath) {
            _gameStateMachine = gameStateMachine;
            _playerDeath = playerDeath;
            _playerDeath.OnDead += PlayerDead;
        }

        void PlayerDead() {
            ExitLevel();
        }

        void ExitLevel() {
            _gameStateMachine.Enter<MainMenuState, SceneName>(SceneName.MainMenu);
        }
    }
}