using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.Gameplay.Actors.Building
{
    public interface IRegisteredBuildingSite
    {
        public bool TryGetProductionService(out IProductionService productionService);
    }
}