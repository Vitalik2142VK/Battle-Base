using System;
using BattleBase.DI;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.ShopSystem
{
    public class ShopSelectedUnitRenderingView : MonoBehaviour, IInjectable
    {
        [SerializeField] private RenderingModelInstaller _renderingModelInstaller;

        private ActorsUpgradeModel _unitsUpgradeModel;
        private TeamColorModel _teamColorModel;

        public IShopUpgradeStatsInfo Info { get; private set; }

        [Inject]
        public void Construct(ActorsUpgradeModel unitsUpgradeModel, TeamColorModel colorModel)
        {
            _unitsUpgradeModel = unitsUpgradeModel ?? throw new ArgumentNullException(nameof(unitsUpgradeModel));
            _teamColorModel = colorModel ?? throw new ArgumentNullException(nameof(colorModel));
        }

        private void OnEnable()
        {
            _unitsUpgradeModel.UnitSelectionChanged += UpdateInfo;
            UpdateInfo();
        }

        private void OnDisable() =>
            _unitsUpgradeModel.UnitSelectionChanged -= UpdateInfo;

        private void UpdateInfo()
        {
            GameObject actor = Instantiate(_unitsUpgradeModel.Selected.CleanPrefab);

            if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                colorChanger.Change(_teamColorModel.PlayerColor);

            _renderingModelInstaller.SetModel(actor);
        }
    }
}