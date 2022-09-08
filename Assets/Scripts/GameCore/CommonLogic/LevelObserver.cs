using GameCore.Player;
using Infrastructure;
using Infrastructure.StateMachine;

namespace GameCore.CommonLogic {
    public sealed class LevelObserver {
        readonly IGameStateMachine _gameStateMachine;

        public LevelObserver(IGameStateMachine gameStateMachine, PlayerDeath playerDeath) {
            _gameStateMachine = gameStateMachine;
            playerDeath.OnDead += PlayerDead;
        }

        void PlayerDead() {
            ExitLevel();
        }

        void ExitLevel() {
            _gameStateMachine.Enter<MainMenuState, SceneName>(SceneName.MainMenu);
        }
    }
}