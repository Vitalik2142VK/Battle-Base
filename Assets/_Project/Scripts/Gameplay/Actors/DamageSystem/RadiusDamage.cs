using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public class RadiusDamage : IDamage
    {
        private readonly IDamage _damage;
        private readonly float _maxDamageRadius;
        private readonly float _radius;

        private float _damageCoefficient;

        public RadiusDamage(IDamage damage, float maxDamageRadius, float radius)
        {
            if (maxDamageRadius < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDamageRadius));

            if (radius < maxDamageRadius)
                throw new ArgumentOutOfRangeException(nameof(radius));

            _damage = damage ?? throw new ArgumentNullException(nameof(damage));
            _maxDamageRadius = maxDamageRadius;
            _radius = radius;
            _damageCoefficient = 0;
        }

        public DamageMask DamageMask => _damage.DamageMask;

        public float Value => _damage.Value * _damageCoefficient;

        public bool HasPriority(ActorMask actorMask, out float damageCoefficient) =>
            _damage.HasPriority(actorMask, out damageCoefficient);

        public void CalculateDamage(float distance)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance));

            if (distance > _radius)
            {
                _damageCoefficient = 0f;

                return;
            }

            if (distance < _maxDamageRadius)
            {
                _damageCoefficient = 1f;

                return;
            }

            _damageCoefficient = 1 - distance / _radius;
        }
    }
}