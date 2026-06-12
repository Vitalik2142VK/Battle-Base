using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public interface IHealth : IDamageble, IActorComponent, IHealthEvents
    {
        public DamageMask DamageMask { get; }

        public bool IsAlive { get; }
    }
}