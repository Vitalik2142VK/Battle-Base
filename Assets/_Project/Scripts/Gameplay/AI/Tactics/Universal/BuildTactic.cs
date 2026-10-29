using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.AI.Deactevators;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public class BuildTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _buildings;
        private readonly IBuildingSitesController _controller;
        private readonly IBuildTacticSetting _setting;
        private readonly ITacticTool _tool;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private int _score;
        private int _numberUnderConstruction;
        private bool _canAction;

        public BuildTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IBuildTacticSetting setting)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            _buildings = new List<IRegisteredBuildingSite>();
            _score = _setting.MaxScore;
            _numberUnderConstruction = 0;
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
            if (_buildings.Count >= _setting.MaxBuildings)
            {
                _score = 0;
                _canAction = false;

                return;
            }

            UpdateScore();

            _canAction = TryBuild();
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

            foreach (var factory in _buildings)
                factory.ActorMissing -= OnRemoveFactory;

            _buildings.Clear();
        }

        private bool TryBuild()
        {
            if (_buildings.Count + _numberUnderConstruction >= _setting.MaxBuildings)
                return false;

            if (TryFindFreeBuildingSite(out IRegisteredBuildingSite site))
            {
                if (TryFindProductionOption(site))
                    return true;
            }

            return false;
        }

        private bool TryFindFreeBuildingSite(out IRegisteredBuildingSite site)
        {
            site = null;

            foreach (var lineNumber in _setting.PriorityLinesNumbers)
            {
                if (_controller.TryGetRandomFreeSiteInLine(lineNumber, out site))
                    return true;
            }

            if (_controller.TryGetRandomFreeSite(out site))
                return true;

            return false;
        }

        private bool TryFindProductionOption(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite.TryGetProductionStorage(out IProductionStorage productionStorage) == false)
                return false;

            return _tool.TryFindSpawnProduction(
                productionStorage,
                _setting.BuildingId,
                out _currentProductionOption);
        }

        private void OnRemoveFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_buildings.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveFactory;
                _buildings.Remove(buildingSite);

                UpdateScore();
            }
        }

        private void OnBuildedFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (buildingSite.CurrentActorId != _setting.BuildingId)
                return;

            UpdateScore();

            _numberUnderConstruction--;

            if (_numberUnderConstruction < 0)
                _numberUnderConstruction = 0;

            _buildings.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveFactory;
        }

        private void UpdateScore()
        {
            if (_buildings.Count == 0)
            {
                _score = _setting.MaxScore;

                return;
            }

            _score = _setting.MaxScore - _buildings.Count * _setting.ScoreForBuild;

            if (_score < _setting.MinScore)
                _score = _setting.MinScore;
        }
    }
}