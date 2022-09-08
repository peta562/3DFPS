using System.Collections.Generic;
using Infrastructure;
using UnityEngine;

namespace Configs.LevelConfig {
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 2)]
    public class LevelConfig : ScriptableObject {
        public SceneName SceneName;
        public List<EnemySpawnerConfig> EnemySpawnerConfigs;
        public Vector3 InitialPlayerPosition;
    }
}