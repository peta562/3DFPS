using Data;
using Infrastructure.Services.Input;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerMove : MonoBehaviour {
        [SerializeField] CharacterController CharacterController;

        float _movementSpeed;
        
        IInputService _inputService;

        public void Init(IInputService inputService, PlayerPositionData playerPositionData, float movementSpeed) {
            CharacterController = GetComponent<CharacterController>();

            transform.position = playerPositionData.Position;
                
            _inputService = inputService;
            _movementSpeed = movementSpeed;
        }

        public void TryMove() {
            var inputVector = _inputService.InputAxis;
            var movementVector = Vector3.zero;

            if ( inputVector.sqrMagnitude > PlayerConstants.Epsilon ) {
                movementVector.x = inputVector.x;
                movementVector.z = inputVector.z;
            }

            movementVector += Physics.gravity;

            CharacterController.Move(transform.TransformDirection(movementVector) * _movementSpeed * Time.deltaTime);
        }

        public void SaveData(PlayerSaveData playerSaveData) {
            playerSaveData.PlayerPositionData.Position = transform.position;
        }
    }
}