using GameCore.CommonLogic;
using UnityEngine;

namespace UI {
    public sealed class ActorUI : MonoBehaviour {
        [SerializeField] HealthBar HealthBar;

        IHealth _health;

        void Start() {
            var health = GetComponent<IHealth>();

            if ( health != null ) {
                Init(health);
            }
        }
        
        public void Init(IHealth health) {
            _health = health;

            _health.HealthChanged += UpdateHealthBar;
        }

        void OnDestroy() {
            _health.HealthChanged -= UpdateHealthBar;
        }

        void UpdateHealthBar() {
            HealthBar.SetValue(_health.CurrentHP, _health.MaxHP);
        }
    }
}