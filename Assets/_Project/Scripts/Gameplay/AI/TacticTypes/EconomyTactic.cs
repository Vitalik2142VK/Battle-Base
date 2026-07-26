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
        private readonly TeamType _teamType;
        private readonly Random _random;

        private IProductionOption _currentProductionOption;
        private int _currentNumberAction;

        public EconomyTactic(
            IBuildingSitesController controller,
            IEconomyTacticSetting setting,
            IMaterialData materialData,
            TeamType teamType)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));
            _materialData = materialData ?? throw new ArgumentNullException(nameof(setting));
            _teamType = teamType;

            _factories = new List<IRegisteredBuildingSite>();
            _random = new Random();
            _currentNumberAction = 0;
        }

        public TacticType Type => TacticType.Economy;

        public bool CanAction()
        {
            if (_materialData.CurrentMaterials > _setting.MaterialsForStop ||
                _currentNumberAction >= _setting.NumberActionsRow)
            {
                _currentNumberAction = 0;

                return false;
            }

            if (_factories.Count < _setting.MaxFactories)
            {
                if (TryCreateFactory())
                    return true;
            }


            return false;
        }

        public ICommand GetCommand()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private bool TryCreateFactory()
        {
            IRegisteredBuildingSite freeBuildingSite = null;

            foreach (var lineNumber in _setting.LineNumbersForBuild)
            {
                IRegisteredBuildingSite[] freeBuildingSites =
                    _controller.GetFreeRegisteredBuildingSites(_teamType, lineNumber);

                if (freeBuildingSites.Length == 0)
                    continue;

                int randomBuildingSite = _random.Next(0, freeBuildingSites.Length);
                freeBuildingSite = freeBuildingSites[randomBuildingSite];
            }

            if (freeBuildingSite != null)
            {
                if (TryFindProductionOption(freeBuildingSite))
                    return true;
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
    }
}