using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public class TacticTool : ITacticTool
    {
        private readonly IMaterialRegistry _materialRegistry;

        private IMaterialData _materialData;

        public TacticTool(IMaterialRegistry materialRegistry)
        {
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public void Init(TeamType team)
        {
            _materialData ??= _materialRegistry.GetMaterialData(team);
        }

        public bool TryFindSpawnProduction(
            IProductionStorage productionStorage, 
            string actorId, 
            out IProductionOption option)
        {
            option = null;

            if (productionStorage == null)
                throw new ArgumentNullException(nameof(productionStorage));

            if (string.IsNullOrEmpty(actorId))
                throw new ArgumentException(nameof(actorId));

            IEnumerable<IProductionOption> productionOptions = productionStorage.GetProductionOptions();

            foreach (var productionOption in productionOptions)
            {
                if (TryFindFactoryData(productionOption, actorId) && CanBuy(productionOption))
                {
                    option = productionOption;

                    return true;
                }
            }

            return false;
        }

        public bool TryFindImproveProduction(IProductionStorage productionStorage, out IProductionOption option)
        {
            option = null;

            if (productionStorage == null)
                throw new ArgumentNullException(nameof(productionStorage));

            foreach (var productionOption in productionStorage.GetProductionOptions())
            {
                if (productionOption.Type != TypeProduction.Improve)
                    continue;

                if (CanBuy(productionOption))
                {
                    option = productionOption;

                    return true;
                }
            }

            return false;
        }

        private bool TryFindFactoryData(IProductionOption productionOption, string actorId)
        {
            if (productionOption.Type != TypeProduction.Spawn)
                return false;

            if (productionOption.Data is IActorData actorData == false)
                return false;

            return actorData.Id == actorId;
        }

        private bool CanBuy(IProductionOption productionOption) =>
            _materialData.CanSpend(productionOption.Data.Price);
    }
}