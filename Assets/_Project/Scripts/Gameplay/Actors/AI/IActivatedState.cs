namespace BattleBase.Gameplay.Actors.AI
{
    public interface IActivatedState
    {
        public IActorState ActorState { get; }

        public bool CanAction { get; }
    }
}