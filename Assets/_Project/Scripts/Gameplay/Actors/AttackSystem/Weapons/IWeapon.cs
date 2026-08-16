using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public interface IWeapon : IUpdateable
    {
        public IWeaponConfig Config { get; }

        public bool CanAttack { get; }

        public bool IsReloaded { get; }

        public void Init(IProjectileController projectileController);

        public void Enable();

        public void AttackTarget(ITarget target);

        public void Upgrade(IWeaponConfigModificator modificator);
    }
}