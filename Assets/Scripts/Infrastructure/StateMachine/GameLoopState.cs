namespace Infrastructure.StateMachine {
    public sealed class GameLoopState : IState{
        readonly GameStateMachine _stateMachine;

        public GameLoopState(GameStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }
        public void Enter() {
            throw new System.NotImplementedException();
        }

        public void Exit() {
            throw new System.NotImplementedException();
        }
    }
}