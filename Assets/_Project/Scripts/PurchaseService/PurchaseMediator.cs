using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.AuthService;

namespace BattleBase.PurchaseService
{
    public sealed class PurchaseMediator : IDisposable
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IReadOnlyList<IPurchaseHandler> _handlers;
        private readonly IAuthorizationService _authorizationService;

        public PurchaseMediator(
            IPurchaseService purchaseService,
            IEnumerable<IPurchaseHandler> handlers,
            IAuthorizationService authorizationService)
        {
            _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));
            _handlers = handlers?.ToList() ?? throw new ArgumentNullException(nameof(handlers));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));

            _purchaseService.PurchaseCompleted += OnPurchaseCompleted;
            _authorizationService.SDKDataReceived += OnSDKDataReceived;

            if (_authorizationService.IsSDKDataReceived)
                _purchaseService.RestorePendingPurchases();
        }

        public void Dispose()
        {
            _purchaseService.PurchaseCompleted -= OnPurchaseCompleted;
            _authorizationService.SDKDataReceived -= OnSDKDataReceived;
        }

        private void OnPurchaseCompleted(PurchaseConfig config)
        {
            foreach (var handler in _handlers)
            {
                if (handler.CanHandle(config))
                    handler.Grant(config);
            }
        }

        private void OnSDKDataReceived() =>
            _purchaseService.RestorePendingPurchases();
    }
}