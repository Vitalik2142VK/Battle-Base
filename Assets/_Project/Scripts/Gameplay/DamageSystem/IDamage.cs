using BattleBase.Gameplay.Units;

namespace BattleBase.Gameplay.DamageSystem
{
    public interface IDamage
    {
        public DamageMask DamageMask { get; }

        public float Value { get; }
    }
}