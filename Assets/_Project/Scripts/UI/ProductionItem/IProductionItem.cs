using BattleBase.Gameplay.Actors.Production;
using System;
using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public event Action<IProductionData> ItemClicked;

        public void SetInfo(IProductionOption productionOption);

        public void SetParent(Transform parent);

        public void ResetParent();
    }
}