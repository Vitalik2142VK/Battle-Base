using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.AI.Deactevators;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public class DemolitionByEconomyTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _buildings;
        private readonly IBuildingSitesController _controller;
        private readonly IDemolitionByEconomyTacticSetting _setting;
        private readonly IMaterialData _materialData;
        private readonly Randomizer _randomizer;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private int _score;
        private bool _canAction;
        private bool _isDeactivated;

        public DemolitionByEconomyTactic(
            IBuildingSitesController controller,
            IDemolitionByEconomyTacticSetting setting,
            IMaterialData materialData,
            Randomizer randomizer)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _materialData = materialData ?? throw new ArgumentNullException(nameof(setting));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _buildings = new List<IRegisteredBuildingSite>();
            _score = _setting.MaxScore;
            _canAction = false;
            _isDeactivated = false;

            _controller.SitesBuildCompleted += OnBuildedBuilding;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public bool IsDeactivated => _tacticDeactivator.IsDeactivate || _isDeactivated;

        public void Init(ITacticDeactivator tacticDeactivator)
        {
            _tacticDeactivator ??= tacticDeactivator ?? throw new ArgumentNullException(nameof(tacticDeactivator));
        }

        public void CalculateScore()
        {
            if (_buildings.Count <= _setting.NumberRemainingFactories ||
                _materialData.CurrentMaterials < _setting.MaterialsForStart)
            {
                _score = 0;
                _canAction = false;

                return;
            }

            _score = _setting.MaxScore;
            _canAction = TryDemolishFactory();
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildedBuilding;

            foreach (var factory in _buildings)
                factory.ActorMissing -= OnRemoveBuilding;

            _buildings.Clear();
        }

        private bool TryDemolishFactory()
        {
            int randomIndex = _randomizer.GetRangeZero(_buildings.Count);
            IRegisteredBuildingSite randomFactory = _buildings[randomIndex];

            if (randomFactory.TryGetProductionStorage(out IProductionStorage storage) == false)
                return false;

            foreach (var productionOption in storage.GetProductionOptions())
            {
                if (productionOption.Type == TypeProduction.Removal)
                {
                    _currentProductionOption = productionOption;

                    return true;
                }
            }

            return false;
        }

        private void OnRemoveBuilding(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_buildings.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveBuilding;
                _buildings.Remove(buildingSite);
            }

            if (_buildings.Count <= _setting.NumberRemainingFactories)
            {
                _score = 0;

                if (_setting.IsRemoveAfterComplited)
                    _isDeactivated = true;
            }
        }

        private void OnBuildedBuilding(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (buildingSite.CurrentActorId != _setting.BuildingId)
                return;

            _buildings.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveBuilding;
        }
    }
}