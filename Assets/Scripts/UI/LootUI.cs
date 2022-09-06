using Data;
using TMPro;
using UnityEngine;

namespace UI {
    public sealed class LootUI : MonoBehaviour {
        [SerializeField] TMP_Text LootText;

        PlayerLootData _playerLootData;

        void Start() {
            UpdateText();
        }

        public void Init(PlayerLootData playerLootData) {
            _playerLootData = playerLootData;
            _playerLootData.Changed += UpdateText;
        }

        void OnDestroy() {
            _playerLootData.Changed -= UpdateText;
        }

        void UpdateText() {
            LootText.text = $"{_playerLootData.Collected}";
        }
    }
}