using UnityEngine;

namespace Infrastructure.Services.Input {
    public sealed class MobileInputService : InputService {
        public override Vector3 InputAxis => SimpleInputAxis();
        public override Vector2 MouseAxis => SimpleMouseAxis();
        
    }
}