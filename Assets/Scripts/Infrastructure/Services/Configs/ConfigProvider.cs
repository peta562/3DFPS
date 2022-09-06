using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.LevelConfig;
using Configs.Windows;
using GameCore.CommonLogic;
using Infrastructure.Services.WindowService;
using UnityEngine;

namespace Infrastructure.Services.Configs {
    public sealed class ConfigProvider : IConfigProvider {
        Dictionary<EnemyTypeId, EnemyConfig> _enemyConfigs;
        Dictionary<WeaponTypeId, WeaponConfig> _weaponConfigs;
        Dictionary<SceneName, LevelConfig> _levelConfigs;
        Dictionary<WindowType, WindowDescription> _windowDescriptions;

        public void LoadConfigs() {
            _enemyConfigs = Resources.LoadAll<EnemyConfig>("Configs/Enemies")
                .ToDictionary(x => x.EnemyTypeId, x => x);
            _weaponConfigs = Resources.LoadAll<WeaponConfig>("Configs/Weapons")
                .ToDictionary(x => x.WeaponTypeId, x => x);
            _levelConfigs = Resources.LoadAll<LevelConfig>("Configs/Levels")
                .ToDictionary(x => x.SceneName, x => x);
            _windowDescriptions = Resources.Load<WindowConfig>("Configs/Windows/WindowConfig")
                .WindowDescriptions
                .ToDictionary(x => x.WindowType, x => x);
        }

        public EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId) =>
            _enemyConfigs.TryGetValue(enemyTypeId, out var enemyConfig) ? enemyConfig : null;

        public WeaponConfig GetWeaponConfig(WeaponTypeId weaponTypeId) =>
            _weaponConfigs.TryGetValue(weaponTypeId, out var weaponConfig) ? weaponConfig : null;

        public LevelConfig GetLevelConfig(SceneName sceneName) =>
            _levelConfigs.TryGetValue(sceneName, out var levelConfig) ? levelConfig : null;

        public WindowDescription GetWindowConfig(WindowType windowType) =>
            _windowDescriptions.TryGetValue(windowType, out var windowDescription) ? windowDescription : null;
    }
}