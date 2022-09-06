using Infrastructure.Services.WindowService;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ShowWindowButton : MonoBehaviour {
        [SerializeField] Button ShowButton;
        [SerializeField] WindowType WindowType;
        
        IWindowManager _windowManager;

        public void Init(IWindowManager windowManager) {
            _windowManager = windowManager;
        }
        
        void Awake() {
            ShowButton.onClick.AddListener(Show);
        }

        void Show() {
            _windowManager.Show(WindowType);
        }
    }
}