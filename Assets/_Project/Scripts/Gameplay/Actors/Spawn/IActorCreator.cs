namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorCreator
    {
        public Actor Create(ActorView view, IActorConfig config);
    }
}