namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionOption
    {
        public TypeProduction Type { get; }

        public IProductionData Data { get; }

        public void Execute();
    }
}