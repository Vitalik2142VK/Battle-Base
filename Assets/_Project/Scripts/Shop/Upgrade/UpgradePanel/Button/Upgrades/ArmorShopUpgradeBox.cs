using System;
using BattleBase.DI;
using BattleBase.UI.Buttons;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ArmorShopUpgradeBox : MonoBehaviour, IInjectable
    {
        [SerializeField] private ShopUpgradeButton _upgradeButton;

        private CreditsModel _credits;
        private ActorsUpgradeModel _unitsUpgradeModel;

        private IUpgradeInfo Info => _unitsUpgradeModel.PanelInfo.HealthInfo;

        [Inject]
        public void Construct(CreditsModel credits, ActorsUpgradeModel unitsUpgradeModel)
        {
            _credits = credits ?? throw new ArgumentNullException(nameof(credits));
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
        }

        private void OnEnable()
        {
            _credits.Changed += UpdateInfo;
            _unitsUpgradeModel.ArmorLevelChanged += UpdateInfo;
            _unitsUpgradeModel.UnitSelectionChanged += UpdateInfo;
            _upgradeButton.Clicked += OnClick;
            UpdateInfo();
        }

        private void OnDisable()
        {
            _credits.Changed -= UpdateInfo;
            _unitsUpgradeModel.ArmorLevelChanged -= UpdateInfo;
            _unitsUpgradeModel.UnitSelectionChanged -= UpdateInfo;
            _upgradeButton.Clicked -= OnClick;
        }

        private void UpdateInfo() =>
            _upgradeButton.UpdateInfo(Info);

        private void OnClick(ButtonClickHandler _)
        {
            IUpgradeInfo info = Info;
            bool isFullStack = info.CurrentLevel >= info.MaximumLevel;

            if (isFullStack == false && _credits.TrySpend(info.CurrentPrice))
            {
                _unitsUpgradeModel.IncreaseArmorLevel();
                _upgradeButton.UpdateInfo(info);
            }
        }
    }
}