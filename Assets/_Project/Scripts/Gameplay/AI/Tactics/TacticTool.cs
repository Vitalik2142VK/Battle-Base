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

        public int TryFindSpawnProductions(
            IProductionStorage storage,
            IEnumerable<string> ids,
            IProductionOption[] options)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (options.Length == 0)
                return 0;

            IEnumerable<IProductionOption> productionOptions = storage.GetProductionOptions();
            int count = 0;

            foreach (var productionOption in productionOptions)
            {
                foreach (var id in ids)
                {
                    if (HasActorDataById(productionOption, id))
                    {
                        options[count++] = productionOption;

                        break;
                    }
                }

                if (count >= options.Length)
                    break;
            }

            return count;
        }

        public bool TryFindSpawnProduction(IProductionStorage storage, string actorId, out IProductionOption option)
        {
            option = null;

            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            if (string.IsNullOrEmpty(actorId))
                throw new ArgumentException(nameof(actorId));

            IEnumerable<IProductionOption> productionOptions = storage.GetProductionOptions();

            foreach (var productionOption in productionOptions)
            {
                if (HasActorDataById(productionOption, actorId) && CanBuy(productionOption))
                {
                    option = productionOption;

                    return true;
                }
            }

            return false;
        }

        private bool HasActorDataById(IProductionOption productionOption, string actorId)
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