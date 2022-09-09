using GameCore.CommonLogic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Configs {
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject {
        public EnemyTypeId EnemyTypeId;

        [Range(1f, 10f)] 
        public float Speed;
        
        public float StoppingDistance;
        
        [Range(1f, 100f)]
        public float Health;
        
        [Range(1f, 20f)]
        public float Damage;
        
        public float AttackDistance;
        
        [Range(0.5f, 5f)]
        public float AttackCooldown;

        public int MaxLoot;
        public int MinLoot;

        public AssetReferenceGameObject PrefabReference;
    }
}