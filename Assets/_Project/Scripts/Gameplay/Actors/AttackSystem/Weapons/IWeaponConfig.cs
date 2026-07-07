using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    public interface IWeaponConfig : IWeaponRange
    {
        public IProjectileConfig ProjectileConfig { get; }

        public IDamageConfig DamageConfig { get; }

        public float RateShooting { get; }

        public float SpeedReload { get; }

        public int NumberShells { get; }
    }
}