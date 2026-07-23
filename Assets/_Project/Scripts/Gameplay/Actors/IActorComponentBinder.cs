namespace BattleBase.Gameplay.Actors
{
    public interface IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view);
    }
}
