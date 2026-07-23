using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public interface IHealth : IDamageble, IActorComponent, IHealthEvents
    {
        public ActorMask ActorMask { get; }

        public bool IsAlive { get; }
    }
}