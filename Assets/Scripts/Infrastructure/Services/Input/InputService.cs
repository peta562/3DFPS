using UnityEngine;

namespace Infrastructure.Services.Input {
    public abstract class InputService : IInputService {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        
        const string MouseNewX = "MouseNew X";
        const string MouseNewY = "MouseNew Y";
        const string AttackButton = "Fire1";
        
        public abstract Vector3 InputAxis { get; }
        public abstract Vector2 MouseAxis { get; }
        
        public bool IsAttackButtonUp() => UnityEngine.Input.GetButtonUp(AttackButton);

        protected Vector3 SimpleInputAxis() => 
            new Vector3(SimpleInput.GetAxis(Horizontal), 0, SimpleInput.GetAxis(Vertical));

        protected Vector2 SimpleMouseAxis() => 
            new Vector2(SimpleInput.GetAxis(MouseNewX), SimpleInput.GetAxis(MouseNewY));
    }
}