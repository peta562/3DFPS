using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public sealed class HealthBar : MonoBehaviour {
        [SerializeField] Image HealthImage;

        public void SetValue(float current, float max) {
            HealthImage.fillAmount = current / max;
        }
    }
}