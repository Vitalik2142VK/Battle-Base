using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public interface IProjectile
    {
        public void SetProjectileConfig(IProjectileConfig config);

        public void SetDamage(IDamage damage);

        public void ShootTarget(IShotPointTransform shotPointTransform, ITarget target);
    }
}