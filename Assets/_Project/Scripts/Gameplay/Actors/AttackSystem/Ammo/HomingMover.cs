using BattleBase.Utils.Extensions;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class HomingMover : MonoBehaviour, IAdvancedIProjectileMover
    {
        [SerializeField]
        [Min(1f)]
        [Tooltip("Радианы/секунда или градусы/секунда в зависимости от выбора")]
        private float _turnRate = 360f;

        [SerializeField][Min(10f)] private float _speed = 40f;
        [SerializeField][Min(0.1f)] private float _maxRange = 30f;
        [SerializeField][Range(0.1f, 2f)] private float _finishDistance = 0.2f;

        private Transform _transform;
        private Vector3 _forward;
        private Vector3 _targetPosition;
        private Vector3 _previousTargetPosition;
        private Vector3 _targetVelocity;
        private float _travelDistance;
        private bool _hasTargetPosition;

        public Vector3 CurrentPosition => _transform.position;

        public bool IsFinished => _travelDistance >= _maxRange || HasReachedTarget();

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _targetVelocity = Vector3.zero;
            _travelDistance = 0f;
            _hasTargetPosition = false;
        }

        public void SetStartPosition(Vector3 startPosition) => 
            _transform.position = startPosition;

        public void SetStartRotation(Quaternion startRotation) =>
            _transform.rotation = startRotation;

        public void SetPointPosition(Vector3 point) =>
            _targetPosition = point;

        public void SetSpeed(float speed)
        {
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed));

            _speed = speed;
        }

        public void Move(float delta)
        {
            if (delta <= 0)
                throw new ArgumentOutOfRangeException(nameof(delta));

            if (_hasTargetPosition == false)
                _forward = _transform.rotation * Vector3.forward;

            UpdateTargetVelocity(delta);

            Vector3 predictedPosition = CalculateInterceptPoint();
            Vector3 desiredDirection = (predictedPosition - _transform.position).normalized;

            _forward = Vector3.RotateTowards(
                _forward,
                desiredDirection,
                Mathf.Deg2Rad * _turnRate * delta,
                0f);

            _transform.rotation = Quaternion.LookRotation(_forward);

            Vector3 movement = _forward * (_speed * delta);

            _transform.position += movement;
            _travelDistance += movement.magnitude;
        }

        private void UpdateTargetVelocity(float delta)
        {
            if (_hasTargetPosition == false)
            {
                _previousTargetPosition = _targetPosition;
                _targetVelocity = Vector3.zero;
                _hasTargetPosition = true;

                return;
            }

            _targetVelocity = (_targetPosition - _previousTargetPosition) / delta;
            _previousTargetPosition = _targetPosition;
        }

        private Vector3 CalculateInterceptPoint()
        {
            float distance = Vector3.Distance(_transform.position, _targetPosition);
            float timeToTarget = distance / _speed;

            return _targetPosition + _targetVelocity * timeToTarget;
        }

        private bool HasReachedTarget()
        {
            return _targetPosition.IsWithinDistance(_transform.position, _finishDistance);
        }
    }
}
