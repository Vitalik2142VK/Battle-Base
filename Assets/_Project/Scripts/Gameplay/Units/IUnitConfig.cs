using BattleBase.Gameplay.HealthSystem;
using BattleBase.Gameplay.Movement;
using BattleBase.Gameplay.Weapons;

namespace BattleBase.Gameplay.Units
{
    public interface IUnitConfig
    {
        public UnitView Prefab { get; }

        public IUnitData Info { get; }

        public IHealthConfig HealthConfig { get; }

        public IWeaponConfig WeaponConfig { get; }

        public IMovementConfig MovementConfig { get; }
    }
}
