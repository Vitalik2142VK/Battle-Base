using BattleBase.Gameplay.Actors.AttackSystem.Missiles;
using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttacker : IActorComponent, IUpdateable, IAttackEvents
    {
        public IWeaponConfig WeaponConfig { get; }

        public void Init(ITargetController targetController, IMissileController missileController);

        public void SetTarget(ITarget target);

        public void SetAim(bool isAiming);

        public void SetAttacking(bool isAttacking);
    }
}