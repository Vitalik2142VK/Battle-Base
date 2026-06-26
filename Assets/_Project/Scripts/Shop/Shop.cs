using System;
using BattleBase.Commands;
using BattleBase.DI;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class Shop : MonoBehaviour, IInjectable
    {
        [SerializeField] private CommandRebuildLayout _commandRebuildLayout;
        [SerializeField] private ShopUnitsScroll _scroll;

        private CreditsModel _credits;
        private UnitsUpgradeModel _unitsUpgradeModel;

        [Inject]
        public void Construct(CreditsModel credits, UnitsUpgradeModel unitsUpgradeModel)
        {
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
        }
    }
}