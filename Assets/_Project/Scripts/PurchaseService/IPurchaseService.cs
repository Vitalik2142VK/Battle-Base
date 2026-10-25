using System;

namespace BattleBase.PurchaseService
{
    public interface IPurchaseService
    {
        public event Action<PurchaseConfig> PurchaseCompleted;

        public void Buy(PurchaseConfig config);

        public void RestorePendingPurchases();
    }
}