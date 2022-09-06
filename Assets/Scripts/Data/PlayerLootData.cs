using System;
using GameCore.Loot;

namespace Data {
    [Serializable]
    public class PlayerLootData {
        public int Collected;
        
        public event Action Changed;

        public void Collect(Loot loot) {
            Collected += loot.Value;
            Changed?.Invoke();
        }
        public void Collect(int value) {
            Collected += value;
            Changed?.Invoke();
        }
        
    }
}