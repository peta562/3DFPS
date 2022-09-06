using GameCore.CommonLogic;
using UnityEngine;

namespace GameCore.Enemy {
    public sealed class EnemyAttack : MonoBehaviour {
        Transform _destinationTransform;
        float _attackCooldown;
        float _attackDistance;
        float _damage;


        float _cooldownTime;
        
        public void Init(Transform destinationTransform, float attackCooldown, float attackDistance, float damage) {
            _destinationTransform = destinationTransform;
            _attackCooldown = attackCooldown;
            _attackDistance = attackDistance;
            _damage = damage;
        }
        
        public void Attack() {
            if ( _cooldownTime > 0f ) {
                ReduceCooldown();
                return;
            }

            if ( !IsInAttackDistance() ) {
                return;
            }
            
            _destinationTransform.GetComponent<IHealth>().TakeDamage(_damage);
            _cooldownTime = _attackCooldown;
        }

        void ReduceCooldown() {
            _cooldownTime -= Time.deltaTime;
        }

        bool IsInAttackDistance() => 
            Vector3.Distance(transform.position, _destinationTransform.position) < _attackDistance;
    }
}