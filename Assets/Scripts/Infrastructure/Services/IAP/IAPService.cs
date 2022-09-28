using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.SaveDataHandler;
using UnityEngine.Purchasing;

namespace Infrastructure.Services.IAP {
    public sealed class IAPService : IIAPService {
        readonly IAPProvider _iapProvider;
        readonly ISaveDataHandler _saveDataHandler;

        public bool IsInitialized => _iapProvider.IsInitialized;
        public event Action Initialized;

        public IAPService(IAPProvider iapProvider, ISaveDataHandler saveDataHandler) {
            _iapProvider = iapProvider;
            _saveDataHandler = saveDataHandler;
        }

        public void Init() {
            _iapProvider.Init(this);
            _iapProvider.Initialized += () => Initialized?.Invoke();
        }

        public List<ProductDescription> GetProducts() =>
            GetProductDescriptions().ToList();

        public void StartPurchase(string productId) =>
            _iapProvider.StartPurchase(productId);

        public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct) {
            var productConfig = _iapProvider.Configs[purchaseProduct.definition.id];

            switch (productConfig.ItemType) {
                case ItemType.Ruby:
                    _saveDataHandler.SaveData.PlayerSaveData.PlayerLootData.Collect(productConfig.Quantity);
                    _saveDataHandler.SaveData.PlayerSaveData.PurchaseData.AddPurchase(purchaseProduct.definition.id);
                    break;
            }

            return PurchaseProcessingResult.Complete;
        }

        IEnumerable<ProductDescription> GetProductDescriptions() {
            var purchaseData = _saveDataHandler.SaveData.PlayerSaveData.PurchaseData;
            
            foreach (var productId in _iapProvider.Products.Keys) {
                var config = _iapProvider.Configs[productId];
                var product = _iapProvider.Products[productId];

                var boughtIAP = purchaseData.GetBoughtIAPById(productId);

                if ( boughtIAP != null && boughtIAP.Count >= config.MaxPurchaseCount ) {
                    continue;
                }

                yield return new ProductDescription() {
                    Id = productId,
                    Config = config,
                    Product = product,
                    AvailablePurchasesLeft = boughtIAP != null
                        ? config.MaxPurchaseCount - boughtIAP.Count
                        : config.MaxPurchaseCount,
                };
            }
        }
    }
}