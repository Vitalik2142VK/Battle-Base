using BattleBase.Utils.Constants;
using BattleBase.Utils.Extensions;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Aim
{
    public class BallisticMuzzle : MonoBehaviour, IAimComponent
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private AnimationCurve _trajectory = AnimationCurve.Linear(0f, 75f, 1f, 35f);
        [SerializeField][Min(1f)] private float _speedRotate = 25f;
        [SerializeField][Min(0.1f)] private float _angleTolerance = 5f;
        [SerializeField][Min(0.1f)] private float _minDistance = 2f;
        [SerializeField][Min(0.1f)] private float _maxDistance = 30f;
        [SerializeField][Range(0.01f, 30f)] private float _returnAngle = 0.1f;

        private Transform _transform;
        private Quaternion _startRotation;

        public bool IsAimed { get; private set; }

        public bool IsRestored => Quaternion.Angle(_transform.localRotation, _startRotation) < _returnAngle;

        private void Awake()
        {
            _transform = transform;
            _startRotation = _transform.localRotation;
        }

        private void OnEnable()
        {
            _transform.localRotation = _startRotation;
        }

        public void LookAtTarget(Vector3 targetPosition, float delta)
        {
            if (delta < 0f)
                throw new System.ArgumentOutOfRangeException(nameof(delta));

            Vector3 localTarget = _parent.InverseTransformPoint(targetPosition);

            float horizontalDistance = Mathf.Abs(localTarget.z);

            if (horizontalDistance < Values.MinDistance)
                return;

            float tick = Mathf.InverseLerp(_minDistance, _maxDistance, horizontalDistance);
            float angle = -_trajectory.Evaluate(tick);

            float targetAngle = -_trajectory.Evaluate(tick);
            CheckAimed(targetAngle);

            if (IsAimed)
                return;

            Quaternion targetRotation = Quaternion.Euler(angle, 0f, 0f);

            _transform.localRotation = Quaternion.RotateTowards(
                _transform.localRotation,
                targetRotation,
                _speedRotate * delta);
        }

        public void ReturnToStart(float delta)
        {
            if (delta < 0f)
                throw new System.ArgumentOutOfRangeException(nameof(delta));

            IsAimed = false;

            _transform.localRotation = Quaternion.RotateTowards(
                _transform.localRotation,
                _startRotation,
                _speedRotate * delta);
        }

        private void CheckAimed(float targetAngle)
        {
            float currentAngle = AngleTools.NormalizeAngle(_transform.localEulerAngles.x);

            IsAimed = Mathf.Abs(currentAngle - targetAngle) <= _angleTolerance;
        }
    }
}