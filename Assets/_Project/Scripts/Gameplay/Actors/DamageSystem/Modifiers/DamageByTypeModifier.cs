using BattleBase.Gameplay.Actors.HealthSystem;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Modifiers
{
    public class DamageByTypeModifier : IDamageModifier
    {
        private readonly IDamageModifier _modifier;
        private readonly ActorMask _actorType;

        public DamageByTypeModifier(IDamageModifier modifier, ActorMask actorType)
        {
            _modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
            _actorType = actorType;
        }

        public float CalculateDamage(IDamage damage, IHealthConfig healthConfig)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            if (damage.HasPriority(_actorType, out float damageCoefficient))
                return damageCoefficient * _modifier.CalculateDamage(damage, healthConfig);
            else
                return _modifier.CalculateDamage(damage, healthConfig);
        }
    }
}