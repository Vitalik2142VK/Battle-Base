namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionService : IActorComponent, IProductionStorage
    {
        public void AddProductionStorage(IProductionStorage productionStorage);
    }
}