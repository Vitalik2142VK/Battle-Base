using BattleBase.Gameplay.Actors.HealthSystem;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Modifiers
{
    public class DamageModifierFactory : IDamageModifierFactory
    {
        public IDamageModifier Create(IHealthComponentSource componentSource)
        {
            if (componentSource == null)
                throw new ArgumentNullException(nameof(componentSource));

            IDamageModifier modifier = new DefaultModifier();
            modifier = new IgnoreArmorModifier(modifier);
            modifier = new DamageByTypeModifier(modifier, componentSource.Type);

            return modifier;
        }
    }
}