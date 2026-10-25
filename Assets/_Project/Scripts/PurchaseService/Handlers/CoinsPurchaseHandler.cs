using System;
using BattleBase.ShopSystem;

namespace BattleBase.PurchaseService
{
    public class CoinsPurchaseHandler : IPurchaseHandler
    {
        private readonly CreditsModel _credits;

        public CoinsPurchaseHandler(CreditsModel credits) =>
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));

        public bool CanHandle(PurchaseConfig config) =>
            config != null
            && config.IsConsumable
            && config.CoinsReward > 0;

        public void Grant(PurchaseConfig config) =>
            _credits.Increase(config.CoinsReward);
    }
}