using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
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
        private readonly Random _random;
        private readonly IRandomTacticSetting _setting;

        private IProductionOption _currentProductionOption;
        private int _score;

        public RandomTactic(IBuildingSitesController controller, IRandomTacticSetting setting)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            _buildingSites = new List<IRegisteredBuildingSite>();
            _productionOptions = new List<IProductionOption>();
            _forbiddenActorIds = new List<string>(_setting.ForbiddenActorIds);
            _random = new Random();
            _score = _setting.MaxScore;
        }

        public TacticCategory Category => _setting.Category;

        public int Score => _score;

        public bool CanAction => _score > _setting.MinScore;

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
            int count = _random.Next(_setting.MinNumSpawn, _setting.MaxNumSpawn);

            _currentProductionOption = null;

            return new MultiProductionActionCommand(productionOption, count);
        }

        private bool TryGetRandomProductions()
        {
            int index;

            do
            {
                index = _random.Next(_buildingSites.Count);

                if (_buildingSites[index].TryGetProductionStorage(out IProductionStorage productionStorage))
                {
                    IProductionOption selected = GetRandomProductionOption(productionStorage);

                    if (selected.Type == TypeProduction.Removal || IsProhibited(selected))
                        continue;

                    _currentProductionOption = selected;

                    return true;
                }

                _buildingSites.RemoveAt(index);
            }
            while (_buildingSites.Count > 0);

            return false;
        }

        private IProductionOption GetRandomProductionOption(IProductionStorage productionStorage)
        {
            _productionOptions.Clear();
            _productionOptions.AddRange(productionStorage.GetProductionOptions());

            int maxIndex = _productionOptions.Count;
            int index = _random.Next(maxIndex);

            return _productionOptions[index];
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