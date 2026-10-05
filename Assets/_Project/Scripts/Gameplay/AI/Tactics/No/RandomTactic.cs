using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Visual.Select;
using BattleBase.Gameplay.AI.Deactevators;
using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.No
{
    public partial class RandomTactic : ITactic
    {
        private readonly List<IRegisteredBuildingSite> _buildingSites;
        private readonly List<IProductionOption> _productionOptions;
        private readonly List<string> _forbiddenActorIds;
        private readonly IBuildingSitesController _controller;
        private readonly IRandomTacticSetting _setting;
        private readonly Randomizer _randomizer;

        private ITacticDeactivator _tacticDeactivator;
        private IProductionOption _currentProductionOption;
        private int _score;

        public RandomTactic(IBuildingSitesController controller, IRandomTacticSetting setting, Randomizer randomizer)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _randomizer = randomizer ?? throw new ArgumentNullException(nameof(randomizer));

            _buildingSites = new List<IRegisteredBuildingSite>();
            _productionOptions = new List<IProductionOption>();
            _forbiddenActorIds = new List<string>(_setting.ForbiddenActorIds);
            _score = _setting.MaxScore;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _score > _setting.MinScore;

        public bool IsDeactivated => _tacticDeactivator.IsDeactivate;

        public void Init(ITacticDeactivator tacticDeactivator)
        {
            _tacticDeactivator ??= tacticDeactivator ?? throw new ArgumentNullException(nameof(tacticDeactivator));
        }

        public void CalculateScore()
        {
            if (_buildingSites.Count == 0)
            {
                var buildingSites = _controller.RegisteredBuildingSites;
                _buildingSites.AddRange(buildingSites);
            }

            if (TryGetRandomProductions())
                _score = _setting.MaxScore;
            else
                _score = _setting.MinScore;
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

        private bool TryGetRandomProductions()
        {
            int indexRandom;
            int lastIndex;
            IRegisteredBuildingSite site;

            do
            {
                indexRandom = _randomizer.GetRangeZero(_buildingSites.Count);
                lastIndex = _buildingSites.Count - 1;
                site = _buildingSites[indexRandom];
                _buildingSites[indexRandom] = _buildingSites[lastIndex];
                _buildingSites.RemoveAt(lastIndex);

                if (site.IsConstruction)
                    continue;

                if (site.TryGetProductionStorage(out IProductionStorage productionStorage))
                {
                    if (TryGetRandomProductionOption(productionStorage, out IProductionOption selected) == false)
                        continue;

                    _currentProductionOption = selected;

                    return true;
                }
            }
            while (_buildingSites.Count > 0);

            return false;
        }

        private bool TryGetRandomProductionOption(
            IProductionStorage productionStorage, 
            out IProductionOption productionOption)
        {
            _productionOptions.Clear();
            _productionOptions.AddRange(productionStorage.GetProductionOptions());
            int indexRandom;
            int lastIndex;

            do
            {
                indexRandom = _randomizer.GetRangeZero(_productionOptions.Count);
                lastIndex = _productionOptions.Count - 1;
                productionOption = _productionOptions[indexRandom];

                if (productionOption.Type == TypeProduction.Removal || IsProhibited(productionOption))
                {
                    _productionOptions[indexRandom] = _productionOptions[lastIndex];
                    _productionOptions.RemoveAt(lastIndex);

                    continue;
                }

                return true;
            }
            while (_productionOptions.Count > 0);

            productionOption = null;

            return false;
            
        }

        private bool IsProhibited(IProductionOption productionOption)
        {
            if (_forbiddenActorIds.Count == 0)
                return false;

            if (productionOption.Data is IActorData actorData == false)
                return false;

            foreach (var id in _forbiddenActorIds)
            {
                if (actorData.Id == id)
                    return true;
            }

            return false;
        }
    }
}