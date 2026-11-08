using BattleBase.Gameplay.Actors.Production;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticTool
    {
        public bool TryFindImproveProduction(IProductionStorage storage, out IProductionOption production);

        public int TryFindSpawnProductions(
            IProductionStorage storage,
            IEnumerable<string> ids,
            IProductionOption[] productionOptions);

        public bool TryFindSpawnProduction(
            IProductionStorage storage,
            string actorId,
            out IProductionOption production);
    }
}