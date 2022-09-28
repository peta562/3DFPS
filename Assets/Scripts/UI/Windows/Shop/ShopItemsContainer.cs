using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.IAP;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine;

namespace UI.Windows.Shop {
    public class ShopItemsContainer : MonoBehaviour {
        const string ShopItemPath = "ShopItem";

        [SerializeField] GameObject[] ShopUnavailableObjects;
        [SerializeField] Transform Parent;

        readonly List<GameObject> _shopItems = new List<GameObject>();
        
        IIAPService _iapService;
        ISaveDataHandler _saveDataHandler;
        IAssetProvider _assetProvider;

        public void Construct(IIAPService iapService, ISaveDataHandler saveDataHandler, IAssetProvider assetProvider) {
            _iapService = iapService;
            _saveDataHandler = saveDataHandler;
            _assetProvider = assetProvider;
        }

        public void Init() =>
            RefreshAvailableItems();

        public void Subscribe() {
            _iapService.Initialized += RefreshAvailableItems;
            _saveDataHandler.SaveData.PlayerSaveData.PurchaseData.Changed += RefreshAvailableItems;
        }

        public void Unsubscribe() {
            _iapService.Initialized -= RefreshAvailableItems;
            _saveDataHandler.SaveData.PlayerSaveData.PurchaseData.Changed -= RefreshAvailableItems;
        }

        async void RefreshAvailableItems() {
            UpdateUnavailableObjects();

            if ( !_iapService.IsInitialized ) {
                return;
            }

            foreach (var shopItem in _shopItems) {
                Destroy(shopItem);
            }

            await FillShopItems();
        }

        async Task FillShopItems() {
            foreach (var productDescription in _iapService.GetProducts()) {
                var shopItemObject = await _assetProvider.Instantiate(ShopItemPath, Parent);
                var shopItem = shopItemObject.GetComponent<ShopItem>();

                shopItem.Construct(productDescription, _iapService, _assetProvider);
                shopItem.Init();

                _shopItems.Add(shopItemObject);
            }
        }

        void UpdateUnavailableObjects() {
            foreach (var unavailableObject in ShopUnavailableObjects) {
                unavailableObject.SetActive(!_iapService.IsInitialized);
            }
        }
    }
}