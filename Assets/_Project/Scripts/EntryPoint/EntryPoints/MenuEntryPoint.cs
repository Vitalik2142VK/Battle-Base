using System;
using BattleBase.AdvService;
using BattleBase.AuthService;
using BattleBase.SaveService;
using BattleBase.UI.Buttons;
using BattleBase.Utils.Constants;
using UnityEngine;
using VContainer;

namespace BattleBase.EntryPoints
{
    public class MenuEntryPoint : EntryPointBase
    {
        [SerializeField] private ButtonClickHandler _authButton;
        [SerializeField] private ButtonClickHandler _noAdsButton;

        private IPurchasesSaver _purchasesSaver;
        private IAdvertisingService _advertisingService;
        private IAuthorizationService _authorizationService;

        [Inject]
        public void Construct(
            IPurchasesSaver purchasesSaver,
            IAdvertisingService advertisingService,
            IAuthorizationService authorizationService)
        {
            _purchasesSaver = purchasesSaver ?? throw new ArgumentNullException(nameof(purchasesSaver));
            _advertisingService = advertisingService ?? throw new ArgumentNullException(nameof(advertisingService));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        protected override void Start()
        {
            base.Start();

            _authorizationService.SDKDataReceived += OnGetSDKData;
            _advertisingService.PurchaseSuccess += OnPurchaseSuccess;
            OnGetSDKData();
            ProcessVisibleAds();
        }

        private void OnDestroy()
        {
            _authorizationService.SDKDataReceived -= OnGetSDKData;
            _advertisingService.PurchaseSuccess -= OnPurchaseSuccess;
        }

        private void OnGetSDKData() =>
            _authButton.SetActive(_authorizationService.IsAuth == false);

        private void OnPurchaseSuccess(string id)
        {
            if (string.Equals(id, PurchasesIds.NoAds))
                _purchasesSaver.SetNoAdsState(true);

            ProcessVisibleAds();
        }

        private void ProcessVisibleAds()
        {
            if (_purchasesSaver.IsNoAds)
            {
                _advertisingService.SetActivityStickyAd(false);
                _noAdsButton.Hide();
            }
        }
    }
}