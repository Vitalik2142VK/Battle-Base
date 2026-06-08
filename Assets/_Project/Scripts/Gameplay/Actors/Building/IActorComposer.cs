namespace BattleBase.Gameplay.Actors.Building
{
    public interface IActorComposer
    {
        public Actor Compose(ActorView view, IActorConfig config, TeamType team);
    }
}