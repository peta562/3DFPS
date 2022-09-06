using System;

namespace Data {
    [Serializable]
    public sealed class SaveData {
        public PlayerSaveData PlayerSaveData;

        public SaveData() {
            PlayerSaveData = new PlayerSaveData();
        }
    }
}