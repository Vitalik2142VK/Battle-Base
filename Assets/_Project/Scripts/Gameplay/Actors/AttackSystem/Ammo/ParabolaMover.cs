using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ParabolaMover : MonoBehaviour, IProjectileMover
    {
        private const float ParabolaMultiplier = 4f;

        [SerializeField][Range(0f, 90f)] private float _launchAngle = 45f;
        [SerializeField][Min(0.1f)] private float _maxHeight = 15f;
        [SerializeField][Min(0.1f)] private float _speed = 25f;
        [SerializeField][Min(0.01f)] private float _heightDistanceFactor = 0.25f;

        private Transform _transform;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private float _distance;
        private float _arcHeight;
        private float _progress;

        public Vector3 CurrentPosition => _transform.position;

        public bool IsFinished => _progress >= 1f;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetStartPosition(Vector3 startPosition)
        {
            _startPosition = startPosition;
            _transform.position = startPosition;
        }

        public void SetPointPosition(Vector3 pointPosition)
        {
            _targetPosition = pointPosition;

            _distance = Vector3.Distance(_startPosition, _targetPosition);

            if (_distance <= Mathf.Epsilon)
            {
                _progress = 1f;
                return;
            }

            float angle = _launchAngle * Mathf.Deg2Rad;

            _arcHeight = Mathf.Min(
                Mathf.Tan(angle) * _distance * _heightDistanceFactor,
                _maxHeight);

            _progress = 0f;
        }

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

            _progress += (_speed * delta) / _distance;
            _progress = Mathf.Clamp01(_progress);

            Vector3 position = Vector3.Lerp(
                _startPosition,
                _targetPosition,
                _progress);

            float heightOffset = CalculateHeightOffset(_progress);

            position.y += heightOffset;

            _transform.position = position;
        }

        private float CalculateHeightOffset(float progress)
        {
            return ParabolaMultiplier * progress * (1f - progress) * _arcHeight;
        }
    }
}