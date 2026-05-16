using System;
using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public event Action<ProductionItem> Clicked;

        public IProductionItemInfo Info { get; }

        public void SetParent(Transform parent);

        public void ResetParent();

        public void SetInfo(IProductionItemInfo info);
    }
}