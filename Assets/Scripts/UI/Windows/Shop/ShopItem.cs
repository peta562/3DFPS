using Infrastructure.Services.AssetManagement;
using Infrastructure.Services.IAP;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Shop {
    public class ShopItem : MonoBehaviour {
        [SerializeField] Button BuyButton;
        [SerializeField] TMP_Text PriceText;
        [SerializeField] TMP_Text QuantityText;
        [SerializeField] TMP_Text AvailableItemLeftText;
        [SerializeField] Image Icon;

        ProductDescription _productDescription;
        IIAPService _iapService;
        IAssetProvider _assetProvider;

        public void Construct(ProductDescription productDescription, IIAPService iapService,
            IAssetProvider assetProvider) {
            _productDescription = productDescription;
            _iapService = iapService;
            _assetProvider = assetProvider;
        }

        public async void Init() {
            BuyButton.onClick.AddListener(OnBuyItemClick);

            PriceText.text = _productDescription.Config.Price;
            QuantityText.text = _productDescription.Config.Quantity.ToString();
            AvailableItemLeftText.text = _productDescription.AvailablePurchasesLeft.ToString();
            Icon.sprite = await _assetProvider.Load<Sprite>(_productDescription.Config.Icon);
        }

        void OnBuyItemClick() {
            _iapService.StartPurchase(_productDescription.Id);
        }
    }
}