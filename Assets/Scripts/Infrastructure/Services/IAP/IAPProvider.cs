using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Infrastructure.Services.IAP {
    public sealed class IAPProvider : IStoreListener {
        const string IAPConfigsPath = "IAP/products";

        public event Action Initialized;
        public Dictionary<string, ProductConfig> Configs { get; private set; }
        public Dictionary<string, Product> Products { get; private set; }

        IStoreController _controller;
        IExtensionProvider _extensions;
        IAPService _iapService;

        public bool IsInitialized => _controller != null && _extensions != null;

        public void Init(IAPService iapService) {
            _iapService = iapService;
            
            Configs = new Dictionary<string, ProductConfig>();
            Products = new Dictionary<string, Product>();
            
            Load();
            
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var config in Configs.Values) {
                builder.AddProduct(config.Id, config.ProductType);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void StartPurchase(string productId) =>
            _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            _controller = controller;
            _extensions = extensions;

            foreach (var product in _controller.products.all) {
                Products.Add(product.definition.id, product);
            }
            
            Initialized?.Invoke();

            Debug.Log("IAP initialization success");
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            Debug.LogError($"IAP initialization failed: {error}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent) {
            Debug.Log($"Process purchase success {purchaseEvent.purchasedProduct.definition.id}");

            return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
            Debug.LogError(
                $"Product {product.definition.id} purchase failed: {failureReason}, transaction id {product.transactionID}");

        void Load() {
            var json = Resources.Load<TextAsset>(IAPConfigsPath).text;
            Configs = JsonUtility.FromJson<ProductConfigWrapper>(json).Configs
                .ToDictionary(x => x.Id, x => x);
        }
    }
}