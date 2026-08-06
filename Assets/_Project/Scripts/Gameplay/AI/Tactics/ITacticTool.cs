using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticTool
    {
        public bool TryFindImproveProduction(IProductionStorage storage, out IProductionOption production);

        public bool TryFindSpawnProduction(
            IProductionStorage storage, 
            string actorId, 
            out IProductionOption production);
    }
}