using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.AI.Deactevators;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Attack
{
    public class SpawnUnitsTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _unitsFactories;
        private readonly List<string> _unitFactoriesIds;
        private readonly List<string> _forbiddenUnitsIds;
        private readonly IBuildingSitesController _controller;
        private readonly ISpawnUnitsTacticSetting _setting;
        private readonly ITacticTool _tool;
        private readonly Randomizer _randomizer;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private int _score;
        private bool _canAction;

        public SpawnUnitsTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            ISpawnUnitsTacticSetting setting,
            Randomizer randomizer)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _unitsFactories = new List<IRegisteredBuildingSite>();
            _unitFactoriesIds = new List<string>(_setting.UnitFactoriesIds);
            _forbiddenUnitsIds = new List<string>(_setting.ForbiddenUnitsIds);
            _score = _setting.MaxScore;
            _canAction = false;

            _controller.SitesBuildCompleted += OnBuildedFactory;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public bool IsDeactivated => _tacticDeactivator.IsDeactivate;

        public void Init(ITacticDeactivator tacticDeactivator)
        {
            _tacticDeactivator ??= tacticDeactivator ?? throw new ArgumentNullException(nameof(tacticDeactivator));
        }

        public void CalculateScore()
        {
            UnityEngine.Debug.Log($"SpawnUnitsTactic.CalculateScore - _unitsFactories.Count = {_unitsFactories.Count}");

            if (_unitsFactories.Count <= 0)
            {
                _score = _setting.MinScore;
                _canAction = false;

                return;
            }

            if (TrySelectRandomProductions())
                _canAction = true;
            else
                _canAction = false;

            _score = _setting.MaxScore;
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            int count = _randomizer.GetRange(_setting.MinNumSpawn, _setting.MaxNumSpawn);

            _currentProductionOption = null;

            return new MultiProductionActionCommand(productionOption, count);
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildedFactory;

            foreach (var factory in _unitsFactories)
                factory.ActorMissing -= OnRemoveFactory;

            _unitsFactories.Clear();
        }

        private bool TrySelectRandomProductions()
        {
            int randomIndex = _randomizer.GetRangeZero(_unitsFactories.Count);
            IRegisteredBuildingSite randomfactory = _unitsFactories[randomIndex];

            if (randomfactory.TryGetProductionStorage(out IProductionStorage productionStorage))
            {
                randomIndex = _randomizer.GetRangeZero(_forbiddenUnitsIds.Count);
                string randomUnitId = _forbiddenUnitsIds[randomIndex];

                if (_tool.TryFindSpawnProduction(productionStorage, randomUnitId, out _currentProductionOption))
                    return true;
            }

            return false;
        }

        private void OnRemoveFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_unitsFactories.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveFactory;
                _unitsFactories.Remove(buildingSite);
            }
        }

        private void OnBuildedFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            foreach (var factoryId in _unitFactoriesIds)
            {
                if (buildingSite.CurrentActorId == factoryId)
                {
                    _unitsFactories.Add(buildingSite);
                    buildingSite.ActorMissing += OnRemoveFactory;

                    return;
                }
            }
        }
    }
}