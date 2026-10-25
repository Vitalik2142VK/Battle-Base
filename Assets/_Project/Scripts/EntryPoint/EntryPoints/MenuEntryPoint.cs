using System;
using BattleBase.AdvService;
using BattleBase.AuthService;
using BattleBase.SaveService;
using BattleBase.UI.Buttons;
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

        private void OnEnable()
        {
            _authorizationService.SDKDataReceived += OnSDKDataReceived;
            _purchasesSaver.NoAdsStateChanged += OnNoAdsStateChanged;

            _authButton.SetActive(false);
            _noAdsButton.Hide();

            if (_authorizationService.IsSDKDataReceived)
                OnSDKDataReceived();
        }

        private void OnDisable()
        {
            _authorizationService.SDKDataReceived -= OnSDKDataReceived;
            _purchasesSaver.NoAdsStateChanged -= OnNoAdsStateChanged;
        }

        private void OnSDKDataReceived()
        {
            _authButton.SetActive(_authorizationService.IsAuth == false);
            ProcessVisibleAds();
        }

        private void OnNoAdsStateChanged(bool _) =>
            ProcessVisibleAds();

        private void ProcessVisibleAds()
        {
            if (_purchasesSaver.IsNoAds)
            {
                _advertisingService.SetActivityStickyAd(false);
                _noAdsButton.Hide();
            }
            else
            {
                _advertisingService.SetActivityStickyAd(true);
                _noAdsButton.Show();
            }
        }
    }
}