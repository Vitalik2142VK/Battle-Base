using System;
using BattleBase.SaveService;
using BattleBase.Utils.Constants;

namespace BattleBase.PurchaseService
{
    public class NoAdsPurchaseHandler : IPurchaseHandler
    {
        private readonly IPurchasesSaver _saver;

        public NoAdsPurchaseHandler(IPurchasesSaver saver) =>
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

        public bool CanHandle(PurchaseConfig config) =>
            config != null
            && config.IsConsumable == false
            && config.Id == PurchasesKeys.NoAdsKey;

        public void Grant(PurchaseConfig config) =>
            _saver.SetNoAdsState(true);
    }
}