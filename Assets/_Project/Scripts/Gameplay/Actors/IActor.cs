namespace BattleBase.Gameplay.Actors
{
    public interface IActor : IUpdateable, ITeamSetter
    {
        public IActorData Data { get; }

        public bool IsEnabled { get; }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent;
    }
}
