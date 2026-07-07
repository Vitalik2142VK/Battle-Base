using System;
using BattleBase.DI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopSelectedUnitPreview : MonoBehaviour, IInjectable
    {
        [SerializeField] private Image _preview;

        private ActorsUpgradeModel _unitsUpgradeModel;

        public IShopUpgradeStatsInfo Info { get; private set; }

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel) =>
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));

        private void OnEnable()
        {
            _unitsUpgradeModel.UnitSelectionChanged += UpdateInfo;
            UpdateInfo();
        }

        private void OnDisable() =>
            _unitsUpgradeModel.UnitSelectionChanged -= UpdateInfo;

        private void UpdateInfo() =>
            _preview.sprite = _unitsUpgradeModel.Selected.Preview;
    }
}