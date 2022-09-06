using GameCore.CommonLogic;
using UnityEngine;

namespace GameCore.Weapons {
    [RequireComponent(typeof(Animator))]
    public sealed class Weapon : MonoBehaviour {
        static readonly int AttackHash = Animator.StringToHash("Attack");
        
        [SerializeField] Transform ShootPoint;
        [SerializeField] Bullet Bullet;

        float _damage;
        float _bulletSpeed;

        Animator _weaponAnimator;

        void Awake() {
            _weaponAnimator = GetComponent<Animator>();
        }

        public void Init(float damage, float bulletSpeed) {
            _damage = damage;
            _bulletSpeed = bulletSpeed;
        }
        public void Attack() {
            _weaponAnimator.SetTrigger(AttackHash);
        }

        void OnAttackAnimationEvent() {
            SpawnBullet();
        }

        void SpawnBullet() {
            var bullet = Instantiate(Bullet);
            bullet.transform.position = ShootPoint.position;
            bullet.Init(_bulletSpeed, ShootPoint.forward, OnHit);
        }

        void OnHit(Collider hit) {
            if ( hit.transform.TryGetComponent<IHealth>(out var healthComponent) == false ) {
                return;
            }
            healthComponent.TakeDamage(_damage);
        }
    }
}