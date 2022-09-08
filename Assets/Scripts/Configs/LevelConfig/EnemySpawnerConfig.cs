using System;
using UnityEngine;

namespace Configs.LevelConfig {
    [Serializable]
    public class EnemySpawnerConfig {
        public Vector3 Position;
        public float SpawnTime;

        public EnemySpawnerConfig(Vector3 position) {
            Position = position;
        }
    }
}