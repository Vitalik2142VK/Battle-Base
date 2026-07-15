using System;
using BattleBase.Gameplay.Map;
using BattleBase.ShopSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class ShopPopUp : PopUp
    {
        [SerializeField] private ShopUnitsScroll[] _scrolls;

        private ActorsUpgradeModel _unitsUpgradeModel;

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel, TeamColorModel teamColorModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
        }

        public override void Init()
        {
            base.Init();

            foreach(ShopUnitsScroll shopUnitsScroll in _scrolls)
                shopUnitsScroll.Init(_unitsUpgradeModel.Infos);
        }
    }
}