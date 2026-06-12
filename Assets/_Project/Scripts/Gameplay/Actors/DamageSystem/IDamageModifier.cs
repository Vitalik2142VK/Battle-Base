using BattleBase.Gameplay.Actors.HealthSystem;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface IDamageModifier
    {
        public float CalculateDamage(IDamage damage, IHealthConfig healthConfig);
    }
}