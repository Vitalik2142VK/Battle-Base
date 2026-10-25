using System;
using BattleBase.DI;
using BattleBase.PurchaseService;
using UnityEngine;
using VContainer;

namespace BattleBase.Commands
{
    public class CommandBuyNoAds : CommandBase, IInjectable
    {
        [SerializeField] private PurchaseConfig _config;

        private IPurchaseService _purchaseService;

        [Inject]
        public void Construct(IPurchaseService purchaseService) =>
           _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));

        public override void Execute()
        {
            if (_config == null)
            {
                Debug.LogError($"{nameof(CommandBuyNoAds)}: config is not assigned on {name}", this);

                return;
            }

            _purchaseService.Buy(_config);
        }
    }
}