namespace BattleBase.Gameplay.Actors
{
    public interface IActor
    {
        public void Add<T>(T component) where T : class, IActorComponent;

        public bool TryGet<T>(out T component) where T : class, IActorComponent;
    }
}
