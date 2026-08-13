using TMPro;
using UnityEngine;
using YG;
using YG.Utils.Pay;

namespace BattleBase.UI.PopUps
{
    public class NoAdsWindow : MonoBehaviour
    {
        [SerializeField] private ImageLoadYG _purchaseImageLoad;
        [SerializeField] private ImageLoadYG _currencyImageLoad;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private string _id = "NoAds";

        private void OnEnable() =>
            UpdateEntries(YG2.PurchaseByID(_id));

        private void UpdateEntries(Purchase data)
        {
            if (data == null)
            {
                Debug.LogError($"No product with ID found: {_id}");

                return;
            }

            _price.text = data.priceValue;

            if (_purchaseImageLoad)
            {
#if UNITY_EDITOR
                if (data.imageURI == InfoYG.DEMO_IMAGE)
                    _purchaseImageLoad.Load(YG.EditorScr.ServerInfo.saveInfo.purchaseImage);
                else
                    _purchaseImageLoad.Load(data.imageURI);
#else
                _purchaseImageLoad.Load(data.imageURI);
#endif
            }

            if (_currencyImageLoad && data.currencyImageURL != string.Empty && data.currencyImageURL != null)
                _currencyImageLoad.Load(data.currencyImageURL);
        }
    }
}