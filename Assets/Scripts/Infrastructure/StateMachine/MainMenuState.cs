using UnityEngine;

namespace Infrastructure.StateMachine {
    public sealed class MainMenuState : IPayloadedState<SceneName> {
        public void Enter(SceneName sceneName) {
            Debug.Log("Success");
        }

        public void Exit() {
        }
    }
}