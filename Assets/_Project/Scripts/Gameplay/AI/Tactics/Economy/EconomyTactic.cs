using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Economy
{
    public class EconomyTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _factories;
        private readonly IBuildingSitesController _controller;
        private readonly IEconomyTacticSetting _setting;
        private readonly IMaterialData _materialData;
        private readonly ITacticTool _tool;

        private IProductionOption _currentProductionOption;
        private int _score;
        private int _numberUnderConstruction;
        private bool _canAction;

        public EconomyTactic(
            ITacticTool tool,
            IBuildingSitesController controller,
            IEconomyTacticSetting setting,
            IMaterialData materialData)
        {
            _tool = tool ?? throw new ArgumentNullException(nameof(tool));
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _materialData = materialData ?? throw new ArgumentNullException(nameof(setting));

            _factories = new List<IRegisteredBuildingSite>();
            _score = _setting.MaxScore;
            _numberUnderConstruction = 0;
            _canAction = false;

            _controller.SiteChanged += OnBuildedFactory;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _canAction;

        public void CalculateScore()
        {
            if (_materialData.CurrentMaterials > _setting.MaterialsForStop)
            {
                _score = 0;

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
            _controller.SiteChanged -= OnBuildedFactory;

            foreach (var factory in _factories)
                factory.ActorMissing -= OnRemoveFactory;

            _factories.Clear();
        }

        private bool TryImprove()
        {
            if (_factories.Count == 0)
                return false;

            foreach (var factory in _factories)
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
            if (_factories.Count + _numberUnderConstruction >= _setting.MaxFactories)
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
                _setting.MaterialFactoryId,
                out _currentProductionOption);
        }

        private void OnRemoveFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (_factories.Contains(buildingSite) == false)
                return;

            if (buildingSite.HasBuilding == false)
            {
                buildingSite.ActorMissing -= OnRemoveFactory;
                _factories.Remove(buildingSite);

                if (_factories.Count < _setting.MaxFactories)
                    _score += _setting.ScoreForBuildFactory;

                if (_score > _setting.MaxScore)
                    _score = _setting.MaxScore;
            }
        }

        private void OnBuildedFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (buildingSite.CurrentId != _setting.MaterialFactoryId)
                return;

            _score -= _setting.ScoreForBuildFactory;
            _numberUnderConstruction--;

            if (_score < _setting.MinScore)
                _score = _setting.MinScore;

            if (_numberUnderConstruction < 0)
                _numberUnderConstruction = 0;

            _factories.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveFactory;
        }
    }
}