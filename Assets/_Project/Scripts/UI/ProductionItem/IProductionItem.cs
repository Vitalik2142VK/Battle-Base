using BattleBase.Gameplay.Actors.Production;
using System;
using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public event Action<IProductionData> ItemClicked;

        public event Action<IProductionData> DecrementClicked;

        public void SetParent(Transform parent);

        public void ResetParent();

        public void SetInfo(IProductionOption productionOption);

        public void SetProgress01(float progress);

        public void SetQuantity(int value);
    }
}