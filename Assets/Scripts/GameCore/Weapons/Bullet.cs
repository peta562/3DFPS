using System;
using GameCore.CommonLogic;
using UnityEngine;

namespace GameCore.Weapons {
    public sealed class Bullet : MonoBehaviour {
        [SerializeField] TriggerObserver TriggerObserver;
        
        float _speed;
        Vector3 _direction;

        Action<Collider> _onHit;

        public void Init(float speed, Vector3 direction, Action<Collider> onHit) {
            TriggerObserver.TriggerEnter += TriggerEnter;
            
            _speed = speed;
            _direction = direction;
            _onHit = onHit;
        }
        
        void Update() {
            transform.position += _direction * _speed * Time.deltaTime;
        }
        
        void TriggerEnter(Collider other) { 
            _onHit?.Invoke(other);
            Destroy(gameObject); 
        }
    }
}