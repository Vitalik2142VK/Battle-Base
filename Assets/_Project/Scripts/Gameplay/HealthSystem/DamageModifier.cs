using BattleBase.Gameplay.DamageSystem;
using System;

namespace BattleBase.Gameplay.HealthSystem
{
    public class DamageModifier : IDamageModifier
    {
        public float CalculateDamage(IDamage damage, IHealthConfig healthConfig)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            if (healthConfig == null)
                throw new ArgumentNullException(nameof(healthConfig));

            float coefficientDamage = 1 - healthConfig.ArmorCoefficient;

            return damage.Value * coefficientDamage;
        }
    }
}