using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class Explosion : MonoBehaviour, IExplosion
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField][Min(0.1f)] private float _maxDamageRadius = 2.5f;
        [SerializeField][Min(1f)] private float _radius = 10f;
        [SerializeField][Range(4, 32)] private int _namberDamagedActors = 16;

        private Collider[] _colliders;

        private void OnValidate()
        {
            if (_radius < _maxDamageRadius)
                _maxDamageRadius = _radius;
        }

        private void Awake()
        {
            _colliders = new Collider[_namberDamagedActors];
        }

        public void Explode(IDamage damage, Vector3 positionExposion)
        {
            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            int numberColliders = Physics.OverlapSphereNonAlloc(
                positionExposion,
                _radius,
                _colliders,
                _layerMask,
                QueryTriggerInteraction.Ignore);

            if (numberColliders == 0)
                return;

            RadiusDamage radiusDamage = new(damage, _maxDamageRadius, _radius);

            for (int i = 0; i < numberColliders; i++)
            {
                if (_colliders[i].TryGetComponent(out ITarget target))
                    HandleDamage(radiusDamage, target, positionExposion);
            }
        }

        private void HandleDamage(RadiusDamage radiusDamage, ITarget target, Vector3 positionExposion)
        {
            float distance = (positionExposion - target.Position).magnitude;
            radiusDamage.CalculateDamage(distance);
            target.TakeDamage(radiusDamage);
        }
    }
}