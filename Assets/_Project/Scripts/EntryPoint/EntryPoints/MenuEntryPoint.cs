using System;
using System.Linq;
using BattleBase.AdvService;
using BattleBase.AuthService;
using BattleBase.Gameplay.Actors;
using BattleBase.SaveService;
using BattleBase.ShopSystem;
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
        private AvailableActorConfigProvider _configSource;

        [Inject]
        public void Construct(
            IPurchasesSaver purchasesSaver,
            IAdvertisingService advertisingService,
            IAuthorizationService authorizationService,
            AvailableActorConfigProvider configSource)
        {
            _purchasesSaver = purchasesSaver ?? throw new ArgumentNullException(nameof(purchasesSaver));
            _advertisingService = advertisingService ?? throw new ArgumentNullException(nameof(advertisingService));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _configSource = configSource ?? throw new ArgumentNullException(nameof(configSource));
        }

        protected override void Start()
        {
            base.Start();

            _authorizationService.SDKDataReceived += OnGetSDKData;
            _advertisingService.PurchaseSuccess += OnPurchaseSuccess;
            OnGetSDKData();
            ProcessVisibleAds();

            Debug.Log("Доступные униты игрока:");
            Debug.Log($"_configSource.ActualPlayerConfigs = {_configSource.ActualPlayerConfigs.ToList().Count}");

            foreach (IActorConfig config in _configSource.ActualPlayerConfigs)
                Debug.Log(config.Data.Id);
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