using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Energy
{
    public class PowerTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _stations;
        private readonly IBuildingSitesController _controller;
        private readonly IPowerTacticSetting _setting;
        private readonly IPowerData _powerData;
        private readonly ITacticTool _tool;

        private IProductionOption _currentProductionOption;
        private int _score;
        private int _numberUnderConstruction;
        private bool _canAction;

        public PowerTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IPowerTacticSetting setting,
            IPowerData powerData)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _powerData = powerData ?? throw new ArgumentNullException(nameof(powerData));

            _stations = new List<IRegisteredBuildingSite>();
            _score = _setting.MaxScore;
            _numberUnderConstruction = 0;
            _canAction = false;

            _controller.SitesBuildCompleted += OnBuildedFactory;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public void CalculateScore()
        {
            if (_powerData.HasMaxCapacity)
            {
                _score = 0;
                _canAction = false;

                return;
            }

            if (TryBuild() || TryImprove())
                _canAction = true;
            else
                _canAction = false;
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;

            if (productionOption.Type == TypeProduction.Spawn)
                _numberUnderConstruction++;

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SitesBuildCompleted -= OnBuildedFactory;

            foreach (var factory in _stations)
                factory.ActorMissing -= OnRemoveFactory;

            _stations.Clear();
        }

        private bool TryImprove()
        {
            if (_stations.Count == 0)
                return false;

            foreach (var factory in _stations)
            {
                if (factory.TryGetProductionStorage(out IProductionStorage productionStorage))
                {
                    if (_tool.TryFindImproveProduction(productionStorage, out _currentProductionOption))
                        return true;
                }
            }

            return false;
        }

        private bool TryBuild()
        {
            if (_stations.Count + _numberUnderConstruction >= _setting.MaxStations)
                return false;

            foreach (var lineNumber in _setting.LineNumbersForBuild)
            {
                if (_controller.TryGetRandomFreeSiteInLine(lineNumber, out IRegisteredBuildingSite site))
                {
                    if (TryFindProductionOption(site))
                        return true;
                }
            }

            return false;
        }

        private bool TryFindProductionOption(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite.TryGetProductionStorage(out IProductionStorage productionStorage) == false)
                return false;

            return _tool.TryFindSpawnProduction(
                productionStorage,
                _setting.PowerStationId,
                out _currentProductionOption);
        }

        private void OnRemoveFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_stations.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveFactory;
                _stations.Remove(buildingSite);

                if (_stations.Count < _setting.MaxStations)
                    _score += _setting.ScoreForBuildStation;

                if (_score > _setting.MaxScore)
                    _score = _setting.MaxScore;
            }
        }

        private void OnBuildedFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (buildingSite.CurrentActorId != _setting.PowerStationId)
                return;

            _score -= _setting.ScoreForBuildStation;
            _numberUnderConstruction--;

            if (_score < _setting.MinScore)
                _score = _setting.MinScore;

            if (_numberUnderConstruction < 0)
                _numberUnderConstruction = 0;

            _stations.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveFactory;
        }
    }
}