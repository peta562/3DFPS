using UnityEngine;

namespace Infrastructure.Services.Input {
    public sealed class StandaloneInputService : InputService {
        const string UnityMouseX = "Mouse X";
        const string UnityMouseY = "Mouse Y";
        
        public override Vector3 InputAxis {
            get {
                var axis = SimpleInputAxis();
                
                if ( axis == Vector3.zero ) {
                    axis = UnityInputAxis();
                }

                return axis;
            }
        }

        public override Vector2 MouseAxis => UnityMouseAxis();

        Vector3 UnityInputAxis() =>
            new Vector3(UnityEngine.Input.GetAxisRaw(Horizontal),0, UnityEngine.Input.GetAxisRaw(Vertical));

        Vector2 UnityMouseAxis() {
            return new Vector2(UnityEngine.Input.GetAxis(UnityMouseX), UnityEngine.Input.GetAxis(UnityMouseY));
        }
    }
}