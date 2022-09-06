using Infrastructure.Services.Ads;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using TMPro;
using UnityEngine;

namespace UI.Windows {
    public sealed class ShopWindow : BaseWindow {
        [SerializeField] RewardedAdItem RewardedAdItem;
        [SerializeField] TMP_Text MoneyText;

        public void Init(IAdService adService, ISaveDataHandler saveDataHandler, IPauseService pauseService) {
            base.Init(saveDataHandler, pauseService);
            RewardedAdItem.Construct(adService, saveDataHandler);
        }
        
        protected override void InitInternal() {
            RewardedAdItem.Init();
            RefreshMoneyText();
        }

        protected override void SubscribeUpdates() {
            RewardedAdItem.Subscribe();
            PlayerSaveData.PlayerLootData.Changed += RefreshMoneyText;
        }

        protected override void UnsubscribeUpdates() {
            RewardedAdItem.Unsubscribe();
            PlayerSaveData.PlayerLootData.Changed -= RefreshMoneyText;
        }

        void RefreshMoneyText() {
            MoneyText.text = PlayerSaveData.PlayerLootData.Collected.ToString();
        }
    }
}