using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.Weapons
{
    public interface IWeaponConfig
    {
        public IDamageConfig DamageConfig { get; }

        public float RateShooting { get; }

        public float SpeedReload { get; }

        public int NumberShells { get; }
    }
}