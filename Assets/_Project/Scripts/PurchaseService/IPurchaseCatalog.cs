using System.Collections.Generic;

namespace BattleBase.PurchaseService
{
    public interface IPurchaseCatalog
    {
        IReadOnlyList<PurchaseConfig> All { get; }

        PurchaseConfig GetById(string id);
    }
}