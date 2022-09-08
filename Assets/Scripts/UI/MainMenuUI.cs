using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public sealed class MainMenuUI : MonoBehaviour {
        public event Action PlayButtonClicked;
        
        [SerializeField] Button PlayButton;

        void Start() {
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }

        void OnDestroy() {
            PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        void OnPlayButtonClicked() {
            PlayButtonClicked?.Invoke();
        }
    }
}