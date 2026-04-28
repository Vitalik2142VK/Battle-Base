using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.DamageSystem
{
    public interface IDamage
    {
        public DamageMask DamageMask { get; }

        public float Value { get; }
    }
}