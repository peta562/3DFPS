using System;
using System.Collections.Generic;

namespace Infrastructure.Services.IAP {
    public interface IIAPService : IService {
        bool IsInitialized { get; }
        event Action Initialized;
        void Init();
        List<ProductDescription> GetProducts();
        void StartPurchase(string productId);
    }
}