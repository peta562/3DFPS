using Configs;
using Configs.LevelConfig;
using Configs.Windows;
using GameCore.CommonLogic;
using Infrastructure.Services.WindowService;

namespace Infrastructure.Services.Configs {
    public interface IConfigProvider : IService {
        void LoadConfigs();

        EnemyConfig GetEnemyConfig(EnemyTypeId enemyTypeId);

        WeaponConfig GetWeaponConfig(WeaponTypeId weaponTypeId);
        
        LevelConfig GetLevelConfig(SceneName sceneName);
        
        WindowDescription GetWindowConfig(WindowType shopWindow);
        PlayerConfig GetPlayerConfig();
    }
}