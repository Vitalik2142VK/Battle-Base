namespace BattleBase.Gameplay.Actors
{
    public interface IUpdateableController : IUpdateable
    {
        public void AddComponent(IActorComponent component);
    }
}
