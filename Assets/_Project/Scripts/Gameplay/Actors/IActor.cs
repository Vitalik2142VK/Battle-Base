namespace BattleBase.Gameplay.Actors
{
    public interface IActor
    {
        public IActorData Data { get; }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent;
    }
}
