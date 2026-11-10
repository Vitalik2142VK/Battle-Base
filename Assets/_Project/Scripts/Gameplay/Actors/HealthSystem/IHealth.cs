using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public interface IHealth : IDamageble, IActorComponent, IHealthEvents
    {
        public ActorMask ActorMask { get; }

        public void Init(IEnalableActor enalableActor);

        public void Upgrade(HealthConfigModificator modificator);
    }
}