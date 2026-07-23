namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IPriorityActorType
    {
        public ActorMask ActorMask { get; }

        public float DamageCoefficient { get; }
    }
}
