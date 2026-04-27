using BattleBase.Gameplay.DamageSystem;

namespace BattleBase.Gameplay.HealthSystem
{
    public interface IDamageModifier
    {
        public float CalculateDamage(IDamage damage, IHealthConfig healthConfig);
    }
}