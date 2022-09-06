﻿using System;

namespace GameCore.CommonLogic {
    public interface IHealth {
        event Action HealthChanged;
        float CurrentHP { get; set; }
        float MaxHP { get; set; }
        void TakeDamage(float damage);
    }
}