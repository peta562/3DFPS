using Data;
using GameCore.CommonLogic;
using UnityEngine;

namespace GameCore.Loot {
    public class LootPiece : MonoBehaviour {
        [SerializeField] TriggerObserver TriggerObserver;
        
        Loot _loot;
        PlayerLootData _playerLootData;

        public void Init(PlayerLootData playerLootData, Loot loot) {
            _playerLootData = playerLootData;
            _loot = loot;

            TriggerObserver.TriggerEnter += TriggerEnter;
        }

        void OnDestroy() {
            TriggerObserver.TriggerEnter -= TriggerEnter;
        }

        void TriggerEnter(Collider other) {
            Pickup();
        }

        void Pickup() {
            _playerLootData.Collect(_loot);

            Destroy(gameObject);
        }
    }
}