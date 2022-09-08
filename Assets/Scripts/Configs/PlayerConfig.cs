using UnityEngine;

namespace Configs {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 1)]
    public class PlayerConfig : ScriptableObject {
        [Range(1f, 3f)] 
        public float AttackCooldown;
        
        [Range(5f, 15f)]
        public float MovementSpeed;
        
        [Range(50f, 100f)]
        public float MouseSensitivity;
    }
}