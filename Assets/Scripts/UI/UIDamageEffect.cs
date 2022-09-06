using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public sealed class UIDamageEffect : MonoBehaviour {
        [SerializeField] Image OverlayDamageImage;
        [SerializeField] float Timer;

        public void Play() {
            StartCoroutine(DamageFlash());
        }

        IEnumerator DamageFlash() {
            OverlayDamageImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(Timer);
            OverlayDamageImage.gameObject.SetActive(false);
        }
    }
}