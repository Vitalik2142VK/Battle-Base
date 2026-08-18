using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Gameplay.Actors.DamageSystem;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IAttacker : IActorComponent, IUpdateable, IAttackNotifier
    {
        public IWeaponConfig WeaponConfig { get; }

        public ITargetFinderConfig TargetFinderConfig { get; }

        public void Init(ITargetController targetController, IProjectileController projectileController);

        public void SetTargets(IEnumerable<ITarget> targets);

        public void SetAim(bool isAiming);

        public void SetAttacking(bool isAttacking);

        public void Upgrade(IWeaponConfigModificator modificator);
    }
}