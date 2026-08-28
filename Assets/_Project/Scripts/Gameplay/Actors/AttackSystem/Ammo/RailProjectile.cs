using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(LineMover))]
    public class RailProjectile : Projectile
    {
        [SerializeField][Range(0.5f, 5f)] private float _damageRadiusOnWay = 2.5f;
        [SerializeField][Range(10f, 70f)] private float _distance = 50f;
        [SerializeField][Range(4f, 32f)] private int _maxTargets = 16;
        [SerializeField] private LayerMask _layerMask;

        private List<ITarget> _targets;
        private RaycastHit[] _hits;
        private IProjectileMover _mover;

        public override event Action<Projectile> Deactivated;

        private void Awake()
        {
            _mover = GetComponent<IProjectileMover>();
            _targets = new List<ITarget>();
            _hits = new RaycastHit[_maxTargets];
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);

            if (_mover.IsFinished)
            {
                TakeDamageAll();

                Deactivated?.Invoke(this);
            }
        }

        public override void ShootTarget(IShotPointTransform shotPointTransform, ITarget target)
        {
            if (shotPointTransform == null)
                throw new ArgumentNullException(nameof(shotPointTransform));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (_targets.Count != 0)
                return;

            _mover.SetStartPosition(shotPointTransform.Position);
            _mover.SetPointPosition(target.Position);
            _mover.SetSpeed(Config.Speed);

            FindTargetsOnWay(shotPointTransform.Position, target.Position);
        }

        private void FindTargetsOnWay(Vector3 origin, Vector3 endPosition)
        {
            _targets.Clear();

            Vector3 direction = (origin - endPosition).normalized;
            int count = Physics.SphereCastNonAlloc(
                origin, 
                _damageRadiusOnWay,
                direction,
                _hits,
                _distance,
                _layerMask, 
                QueryTriggerInteraction.Ignore);

            for (int i = 0; i < count; i++)
            {
                if (_hits[i].collider.TryGetComponent(out ITarget target))
                    _targets.Add(target);
            }
        }

        private void TakeDamageAll()
        {
            foreach (var target in _targets)
                target.TakeDamage(Damage);

            _targets.Clear();
        }
    }
}