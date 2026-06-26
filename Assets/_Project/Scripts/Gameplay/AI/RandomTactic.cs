using BattleBase.Core;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public partial class RandomTactic : ITactic
    {
        private readonly List<IRegisteredBuildingSite> _buildingSites;
        private readonly List<ProductionOption> _productionOptions;
        private readonly IBuildingSitesController _controller;
        private readonly Random _random;
        private readonly RandomTacticSetting _setting;

        private IProductionService _currentProductionService;

        public RandomTactic(IBuildingSitesController controller, RandomTacticSetting setting)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            _buildingSites = new List<IRegisteredBuildingSite>();
            _productionOptions = new List<ProductionOption>();
            _random = new Random();
        }

        public bool CanAction()
        {
            if (_buildingSites.Count == 0)
            {
                var buildingSites = _controller.GetRegisteredBuildingSites(_setting.Team);
                _buildingSites.AddRange(buildingSites);
            }

            return TryGetRandomSpawner();
        }

        public ICommand GetCommand()
        {
            if (_currentProductionService == null)
            {
                if (CanAction() == false)
                    throw new InvalidOperationException("Tactics cannot be used");
            }

            ProductionOption productionOption = GetRandomActorData();
            int count = _random.Next(_setting.MinNumSpawn, _setting.MaxNumSpawn);

            return new MultiActionCommand(productionOption, count);
        }

        private bool TryGetRandomSpawner()
        {
            int index;

            do
            {
                index = _random.Next(_buildingSites.Count);

                if (_buildingSites[index].TryGetActorSpawner(out _currentProductionService))
                {
                    return true;
                }
                else
                {
                    _buildingSites.RemoveAt(index);
                }
            }
            while (_buildingSites.Count > 0);

            return false;
        }

        private ProductionOption GetRandomActorData()
        {
            _productionOptions.Clear();
            _productionOptions.AddRange(_currentProductionService.ProductionOptions);

            int index = _random.Next(_productionOptions.Count);

            return _productionOptions[index];
        }
    }
}