using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.TacticTypes
{
    public class EconomyTactic : ITactic, IDisposable
    {
        private readonly List<IRegisteredBuildingSite> _factories;
        private readonly IBuildingSitesController _controller;
        private readonly IEconomyTacticSetting _setting;
        private readonly IMaterialData _materialData;

        private IProductionOption _currentProductionOption;
        private int _score;
        private bool _canAction;

        public EconomyTactic(
            IBuildingSitesController controller,
            IEconomyTacticSetting setting,
            IMaterialData materialData)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _materialData = materialData ?? throw new ArgumentNullException(nameof(setting));

            _factories = new List<IRegisteredBuildingSite>();
            _score = _setting.Score;
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

            if (TryImproveFactory() || TryCreateFactory())
            {
                _canAction = true;
            }
            else
            {
                _score += _setting.ScoreForAction;

                _canAction = false;
            }

            UnityEngine.Debug.Log($"EconomyTactic.CalculateScore == {_score} || CanAction == {CanAction}");
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
                throw new InvalidOperationException("Tactics cannot be used");

            _score -= _setting.ScoreForAction;
            IProductionOption productionOption = _currentProductionOption;
            _currentProductionOption = null;

            UnityEngine.Debug.Log($"EconomyTactic.GetCommand");

            return new DelegateCommand(() => productionOption.Execute());
        }

        public void Dispose()
        {
            _controller.SiteChanged -= OnBuildedFactory;

            foreach (var factory in _factories)
                factory.ActorMissing -= OnRemoveFactory;
        }

        private bool TryImproveFactory()
        {
            if (_factories.Count == 0)
                return false;

            foreach (var factory in _factories)
            {
                if (factory.TryGetProductionStorage(out IProductionStorage productionStorage))
                {
                    if (CanImprove(productionStorage))
                        return true;
                }
            }

            return false;
        }

        private bool CanImprove(IProductionStorage productionStorage)
        {
            foreach (var productionOption in productionStorage.GetProductionOptions())
            {
                if (productionOption.Type != TypeProduction.Improve)
                    continue;

                if (_materialData.CanSpend(productionOption.Data.Price))
                {
                    _currentProductionOption = productionOption;

                    return true;
                }
            }

            return false;
        }

        private bool TryCreateFactory()
        {
            if (_factories.Count >= _setting.MaxFactories)
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

            IEnumerable<IProductionOption> productionOptions = productionStorage.GetProductionOptions();

            foreach (var productionOption in productionOptions)
            {
                if (TryFindFactoryData(productionOption))
                {
                    _currentProductionOption = productionOption;

                    return true;
                }
            }

            return false;
        }

        private bool TryFindFactoryData(IProductionOption productionOption)
        {
            if (productionOption.Type != TypeProduction.Spawn)
                return false;

            if (productionOption.Data is IActorData actorData == false)
                return false;

            return actorData.Id == _setting.MaterialFactoryId;
        }

        private void OnRemoveFactory()
        {
            for (int i = 0; i < _factories.Count; i++)
            {
                if (_factories[i].HasBuilding == false)
                {
                    _factories[i].ActorMissing -= OnRemoveFactory;
                    _factories.RemoveAt(i);
                    i--;
                }
            }
        }

        private void OnBuildedFactory(IRegisteredBuildingSite buildingSite)
        {
            if (buildingSite == null)
                throw new ArgumentNullException(nameof(buildingSite));

            if (buildingSite.CurrentId != _setting.MaterialFactoryId)
                return;

            _factories.Add(buildingSite);
            buildingSite.ActorMissing += OnRemoveFactory;
        }
    }
}