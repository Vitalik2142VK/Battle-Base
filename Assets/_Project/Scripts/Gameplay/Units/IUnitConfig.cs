using BattleBase.Gameplay.Movement;

namespace BattleBase.Gameplay.Units
{
    public interface IUnitConfig
    {
        public UnitView Prefab { get; }

        public IUnitData Info { get; }

        public IHealthConfig HealthConfig { get; }

        public IDamageConfig DamageConfig { get; }

        public IMovementConfig MovementConfig { get; }
    }
}
