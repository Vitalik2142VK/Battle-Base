using BattleBase.Gameplay.Actors.HealthSystem;
using BattleBase.Utils;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Modifiers
{
    public class IgnoreArmorModifier : IDamageModifier
    {
        private readonly IDamageModifier _modifier;

        public IgnoreArmorModifier(IDamageModifier modifier)
        {
            _modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
        }

        public float CalculateDamage(IDamage damage, IHealthConfig healthConfig)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            if (damage.DamageMask.Contains(DamageMask.ArmorPiercing))
                return damage.Value;
            else
                return _modifier.CalculateDamage(damage, healthConfig);
        }
    }
}