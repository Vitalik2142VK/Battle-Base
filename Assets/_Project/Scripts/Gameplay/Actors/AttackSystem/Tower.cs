using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Tower : MonoBehaviour, IAimComponent
    {
        [SerializeField][Min(1f)] private float _speedRotate = 25f;
        [SerializeField][Range(-1f, 1f)] private float _dotAim = 0.99f;

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
            direction.y = 0f;

            CheckAimed(direction);

            if (IsAimed)
                return;

            if (direction.sqrMagnitude < Values.MinDistance)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation,
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