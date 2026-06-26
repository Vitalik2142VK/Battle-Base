using System;
using BattleBase.DI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopUnitPreview : MonoBehaviour, IInjectable
    {
        [SerializeField] private Image _preview;

        private UnitsUpgradeModel _unitsUpgradeModel;

        public IShopUpgradePanelInfo Info { get; private set; }

        [Inject]
        public void Construct(UnitsUpgradeModel unitsUpgradeModel) =>
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