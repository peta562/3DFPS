using Infrastructure.Services.PauseService;
using UnityEngine;

namespace Infrastructure.StateMachine {
    public sealed class GameLoopState : IState{
        readonly GameStateMachine _stateMachine;
        readonly IPauseService _pauseService;

        public GameLoopState(GameStateMachine stateMachine, IPauseService pauseService) {
            _stateMachine = stateMachine;
            _pauseService = pauseService;
        }
        public void Enter() {
            _pauseService.SetPause(false);
            Debug.Log("Enter game loop");
        }

        public void Exit() {
            _pauseService.SetPause(true);
            Debug.Log("Exit game loop");
        }
    }
}