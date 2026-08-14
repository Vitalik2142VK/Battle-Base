namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionService : IActorComponent, IProductionStorage
    {
        public int BuildingSiteId { get; }

        public void AddProductionFactory(IProductionOptionsFactory factory);

        public void SetBuildingSiteId(int id);
    }
}