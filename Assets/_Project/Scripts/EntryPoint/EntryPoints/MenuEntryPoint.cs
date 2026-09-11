using System;
using BattleBase.SaveService;
using BattleBase.UI.Buttons;
using BattleBase.Utils.Constants;
using UnityEngine;
using VContainer;
using YG;

namespace BattleBase.EntryPoints
{
    public class MenuEntryPoint : EntryPointBase
    {
        [SerializeField] private ButtonClickHandler _authButton;
        [SerializeField] private ButtonClickHandler _noAdsButton;

        private IPurchasesSaver _purchasesSaver;

        [Inject]
        public void Construct(IPurchasesSaver purchasesSaver)
        {
            _purchasesSaver = purchasesSaver ?? throw new ArgumentNullException(nameof(purchasesSaver));
        }

        protected override void Start()
        {
            base.Start();

            OnGetSDKData();

            if (_purchasesSaver.GetState(PurchasesKeys.NoAdsKey) == 1)
                ProcessDisabledAds();

            YG2.onGetSDKData += OnGetSDKData;
            YG2.onPurchaseSuccess += OnPurchaseSuccess;
        }

        private void OnDestroy()
        {
            YG2.onGetSDKData -= OnGetSDKData;
            YG2.onPurchaseSuccess -= OnPurchaseSuccess;
        }

        private void OnGetSDKData()
        {
            if (YG2.player.auth)
                _authButton.Hide();
            else
                _authButton.Show();
        }

        private void OnPurchaseSuccess(string id)
        {
            if (string.Equals(id, PurchasesIds.NoAds))
            {
                _purchasesSaver.SetState(PurchasesKeys.NoAdsKey, 1);
                ProcessDisabledAds();
            }
        }

        private void ProcessDisabledAds()
        {
            YG2.StickyAdActivity(false);
            _noAdsButton.Hide();
        }
    }
}