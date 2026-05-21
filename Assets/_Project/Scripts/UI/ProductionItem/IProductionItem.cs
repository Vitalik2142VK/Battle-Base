using BattleBase.Gameplay.Actors;
using System;
using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public event Action<IProductionItem> ItemClicked;

        public IActorData Info { get; }

        public void SetParent(Transform parent);

        public void ResetParent();

        public void SetInfo(IActorData info);
    }
}