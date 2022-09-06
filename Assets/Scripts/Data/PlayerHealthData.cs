﻿using System;

namespace Data {
    [Serializable]
    public class PlayerHealthData {
        public float CurrentHP;
        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;
    }
}