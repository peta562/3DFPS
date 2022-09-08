using Data;
using GameCore.CommonLogic;
using Infrastructure.Services.SaveDataHandler;
using Infrastructure.Services.SaveLoad;

namespace Infrastructure.StateMachine {
    public sealed class LoadProgressState : IState {
        readonly GameStateMachine _stateMachine;
        readonly ISaveDataHandler _saveDataHandler;
        readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, ISaveDataHandler saveDataHandler,
            ISaveLoadService saveLoadService) {
            _stateMachine = stateMachine;
            _saveDataHandler = saveDataHandler;
            _saveLoadService = saveLoadService;
        }

        public void Enter() {
            LoadDataOrCreateNew();

            _stateMachine.Enter<MainMenuState, SceneName>(SceneName.MainMenu);
        }

        public void Exit() {

        }

        void LoadDataOrCreateNew() {
            _saveDataHandler.SaveData = _saveLoadService.LoadData() ?? CreateData();
        }

        SaveData CreateData() {
            var saveData = new SaveData();
            
            saveData.PlayerSaveData.PlayerHealthData.MaxHP = 100f;
            saveData.PlayerSaveData.PlayerHealthData.ResetHP();
            saveData.PlayerSaveData.PlayerWeaponData.WeaponTypeId = WeaponTypeId.IceWand;

            return saveData;
        }
    }
}