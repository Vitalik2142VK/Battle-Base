using BattleBase.Gameplay.Actors.DamageSystem;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public interface IWeaponConfig : IWeaponRange
    {
        public IDamageConfig DamageConfig { get; }

        public float RateShooting { get; }

        public float SpeedReload { get; }

        public int NumberShells { get; }
    }
}