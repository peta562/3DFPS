using GameCore.CommonLogic;
using UnityEngine;

namespace Configs {
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig", order = 0)]
    public class WeaponConfig : ScriptableObject {
        public WeaponTypeId WeaponTypeId;
        
        [Range(10f, 100f)]
        public float Damage;
        
        [Range(20f, 100f)]
        public float BulletSpeed;
        
        public GameObject Prefab;
    }
}