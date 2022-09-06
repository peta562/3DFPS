using Infrastructure.Services.Input;
using UnityEngine;

namespace GameCore.Player {
    public sealed class PlayerCamera : MonoBehaviour {
        Camera _playerCamera;
        float _mouseSensitivity;

        IInputService _inputService;
        
        float _xRotation;

        public void Init(IInputService inputService, Camera playerCamera, float mouseSensitivity) {
            _inputService = inputService;
            _playerCamera = playerCamera;
            _mouseSensitivity = mouseSensitivity;
        }

        public void UpdateCamera() {
            var mouseVector = _inputService.MouseAxis;
            
            if ( mouseVector.sqrMagnitude > PlayerConstants.Epsilon ) {
                var mouseX = mouseVector.x * _mouseSensitivity * Time.deltaTime;
                var mouseY = mouseVector.y * _mouseSensitivity * Time.deltaTime;

                _xRotation -= mouseY;
                _xRotation = Mathf.Clamp(_xRotation, -PlayerConstants.CameraLimitY, PlayerConstants.CameraLimitY);
                
                _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
                transform.Rotate(Vector3.up * mouseX);
            }
        }
    }
}
