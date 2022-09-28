using System;

namespace Data {
    [Serializable]
    public class PlayerSaveData {
        public PlayerPositionData PlayerPositionData;
        public PlayerHealthData PlayerHealthData;
        public PlayerKillData PlayerKillData;
        public PlayerWeaponData PlayerWeaponData;
        public PlayerLootData PlayerLootData;
        public PurchaseData PurchaseData;
        
        public PlayerSaveData() {
            PlayerPositionData = new PlayerPositionData();
            PlayerHealthData = new PlayerHealthData();
            PlayerKillData = new PlayerKillData();
            PlayerWeaponData = new PlayerWeaponData();
            PlayerLootData = new PlayerLootData();
            PurchaseData = new PurchaseData();
        }
    }
}