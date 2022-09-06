using DG.Tweening;
using UnityEngine;

namespace GameCore.Loading {
    public sealed class LoadingScreen : MonoBehaviour {
        [SerializeField] CanvasGroup Screen;

        void Awake() {
            Screen = GetComponent<CanvasGroup>();
            
            DontDestroyOnLoad(this);
        }

        public void Show() {
            gameObject.SetActive(true);
            Screen.alpha = 1;
        }

        public void Hide() {
            Screen
                .DOFade(1f, 0.5f)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}