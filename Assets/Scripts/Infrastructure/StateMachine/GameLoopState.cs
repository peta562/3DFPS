using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Infrastructure.StateMachine {
    public sealed class GameLoopState : IState{
        readonly GameStateMachine _stateMachine;
        readonly IPauseService _pauseService;
        readonly ISaveLoadService _saveLoadService;

        public GameLoopState(GameStateMachine stateMachine, IPauseService pauseService,
            ISaveLoadService saveLoadService) {
            _stateMachine = stateMachine;
            _pauseService = pauseService;
            _saveLoadService = saveLoadService;
        }

        public void Enter() {
            _pauseService.SetPause(false);
            Debug.Log("Enter game loop");
        }

        public void Exit() {
            _pauseService.SetPause(true);
            _saveLoadService.SaveData();
            Debug.Log("Exit game loop");
        }
    }
}