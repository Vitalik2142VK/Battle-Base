using BattleBase.Core;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.TacticTypes
{
    public partial class RandomTactic : ITactic
    {
        private readonly List<IRegisteredBuildingSite> _buildingSites;
        private readonly List<ProductionOption> _productionOptions;
        private readonly IBuildingSitesController _controller;
        private readonly Random _random;
        private readonly RandomTacticSetting _setting;

        private ProductionOption _currentProductionOption;
        private TeamType _teamType;

        public RandomTactic(IBuildingSitesController controller)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = new RandomTacticSetting();

            _buildingSites = new List<IRegisteredBuildingSite>();
            _productionOptions = new List<ProductionOption>();
            _random = new Random();
        }

        public TacticType Type => TacticType.Random;

        public bool CanAction()
        {
            if (_buildingSites.Count == 0)
            {
                var buildingSites = _controller.GetRegisteredBuildingSites(_teamType);
                _buildingSites.AddRange(buildingSites);
            }

            return TryGetRandomProductions();
        }

        public void SetTeamm(TeamType teamType)
        {
            _teamType = teamType;
        }

        public ICommand GetCommand()
        {
            if (_currentProductionOption == null)
            {
                if (CanAction() == false)
                    throw new InvalidOperationException("Tactics cannot be used");
            }

            ProductionOption productionOption = _currentProductionOption;
            int count = _random.Next(_setting.MinNumSpawn, _setting.MaxNumSpawn);

            _currentProductionOption = null;

            return new MultiActionCommand(productionOption, count);
        }

        private bool TryGetRandomProductions()
        {
            int index;

            do
            {
                index = _random.Next(_buildingSites.Count);

                if (_buildingSites[index].TryGetProductionService(out IProductionService productionService))
                {
                    ProductionOption selected = GetRandomProductionOption(productionService);

                    if (selected.Type == TypeProduction.Removal)
                        continue;

                    _currentProductionOption = selected;

                    return true;
                }

                _buildingSites.RemoveAt(index);
            }
            while (_buildingSites.Count > 0);

            return false;
        }

        private ProductionOption GetRandomProductionOption(IProductionStorage productionStorage)
        {
            _productionOptions.Clear();
            _productionOptions.AddRange(productionStorage.GetProductionOptions());

            int maxIndex = _productionOptions.Count;
            int index = _random.Next(maxIndex);

            return _productionOptions[index];
        }
    }
}