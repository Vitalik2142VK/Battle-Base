using BattleBase.Utils.Constants;
using UnityEngine;
using UnityEngine.UIElements;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Muzzle : MonoBehaviour, IAimComponent
    {
        [SerializeField] private Transform _parrent;
        [SerializeField][Range(1f, 720f)] private float _speedRotate = 25f;
        [SerializeField][Range(-1f, 1f)] private float _dotAim = 0.95f;

        private Transform _transform;
        private Quaternion _startRotation;

        public bool IsAimed { get; private set; }

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

            Vector3 direction = targetPosition - _transform.position;
            CheckAimed(direction);

            if (IsAimed)
                return;

            Vector3 localTargetPosition = _parrent.InverseTransformPoint(targetPosition);
            localTargetPosition.x = 0f;

            if (localTargetPosition.sqrMagnitude < Values.MinDistance)
                return;

            float angle = -Mathf.Atan2(localTargetPosition.y, localTargetPosition.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(angle, 0f, 0f);

            _transform.localRotation = Quaternion.RotateTowards(
                _transform.localRotation,
                targetRotation,
                _speedRotate * delta
            );
        }

        private void CheckAimed(Vector3 direction)
        {
            float dot = Vector3.Dot(_transform.forward, direction.normalized);

            IsAimed = dot > _dotAim;
        }
    }
}