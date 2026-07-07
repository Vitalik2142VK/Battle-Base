using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public class Damage : IDamage
    {
        private readonly IDamageConfig _config;

        public Damage(IDamageConfig attributes)
        {
            _config = attributes ?? throw new ArgumentNullException(nameof(attributes));
        }

        public DamageMask DamageMask => _config.DamageMask;

        public float Value => _config.Damage;

        public bool HasPriority(ActorMask actorMask, out float damageCoefficient)
        {
            damageCoefficient = 0f;

            foreach (var priorityActorType in _config.TargetingProfile.PriorityActorTypes)
            {
                if (priorityActorType.ActorMask.Contains(actorMask))
                {
                    damageCoefficient = priorityActorType.DamageCoefficient;

                    return true;
                }
            }

            return false;
        }
    }
}