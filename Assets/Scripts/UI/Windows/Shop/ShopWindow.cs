using Infrastructure.Services.Ads;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.IAP;
using Infrastructure.Services.PauseService;
using Infrastructure.Services.SaveDataHandler;
using TMPro;
using UnityEngine;

namespace UI.Windows.Shop {
    public sealed class ShopWindow : BaseWindow {
        [SerializeField] RewardedAdItem RewardedAdItem;
        [SerializeField] TMP_Text MoneyText;
        [SerializeField] ShopItemsContainer ShopItemsContainer;

        public void Init(IAdService adService, ISaveDataHandler saveDataHandler, IPauseService pauseService,
            IIAPService iapService, IAssetProvider assetProvider) {
            base.Init(saveDataHandler, pauseService);
            RewardedAdItem.Construct(adService, saveDataHandler);
            ShopItemsContainer.Construct(iapService, saveDataHandler, assetProvider);
        }

        protected override void InitInternal() {
            RewardedAdItem.Init();
            ShopItemsContainer.Init();
            RefreshMoneyText();
        }

        protected override void SubscribeUpdates() {
            RewardedAdItem.Subscribe();
            ShopItemsContainer.Subscribe();
            PlayerSaveData.PlayerLootData.Changed += RefreshMoneyText;
        }

        protected override void UnsubscribeUpdates() {
            RewardedAdItem.Unsubscribe();
            ShopItemsContainer.Unsubscribe();
            PlayerSaveData.PlayerLootData.Changed -= RefreshMoneyText;
        }

        void RefreshMoneyText() {
            MoneyText.text = PlayerSaveData.PlayerLootData.Collected.ToString();
        }
    }
}