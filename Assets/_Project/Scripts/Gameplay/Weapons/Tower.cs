using System;
using UnityEngine;

namespace BattleBase.Gameplay.Weapons
{
    public class Tower : MonoBehaviour, ITower
    {
        private const float DotAim = 0.99f;

        [SerializeField] private Muzzle _muzzle;
        [SerializeField][Min(1f)] private float _speedRotate = 25f;

        private ITargetPoint _currentTarget;
        private Transform _transform;
        private bool _isAimed;

        public event Action<bool> Aimed;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (_currentTarget != null)
                LookAtTarget();
        }

        private void OnDestroy()
        {
            if (_currentTarget != null)
                _currentTarget.Destroyed -= OnRemoveTarget;
        }

        public void TakeAim(ITargetPoint target)
        {
            if (_currentTarget != null)
                _currentTarget.Destroyed -= OnRemoveTarget;

            _currentTarget = target ?? throw new ArgumentNullException(nameof(target));
            _currentTarget.Destroyed += OnRemoveTarget;
            _isAimed = false;
        }

        private void LookAtTarget()
        {
            Vector3 direction = _currentTarget.Position - _transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < Muzzle.MinDistance)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation,
                targetRotation,
                _speedRotate * Time.deltaTime
            );

            Vector3 localTargetPosition = _transform.InverseTransformPoint(_currentTarget.Position);
            _muzzle.LookAtTarget(localTargetPosition);

            float dot = Vector3.Dot(_transform.forward, direction.normalized);

            if (_isAimed != dot > DotAim)
            {
                _isAimed = dot > DotAim;

                Aimed?.Invoke(_isAimed);
            }
        }

        private void OnRemoveTarget()
        {
            _currentTarget.Destroyed -= OnRemoveTarget;
            _currentTarget = null;
        }
    }
}