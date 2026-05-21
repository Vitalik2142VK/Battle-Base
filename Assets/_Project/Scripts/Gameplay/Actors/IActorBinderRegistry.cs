namespace BattleBase.Gameplay.Actors
{
    public interface IActorBinderRegistry
    {
        public void Bind(IActor actor, IActorView view);
    }
}
