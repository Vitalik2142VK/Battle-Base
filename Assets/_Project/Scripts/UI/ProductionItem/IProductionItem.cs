using BattleBase.Gameplay.Actors.Production;
using System;
using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public event Action<ProductionOption> ItemClicked;

        public IProductionData Info { get; }

        public void SetParent(Transform parent);

        public void ResetParent();

        public void SetInfo(ProductionOption productionOption);
    }
}