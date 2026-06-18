using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Muzzle : MonoBehaviour
    {
        public const float MinDistance = 0.0001f;

        [SerializeField][Min(1f)] private float _speedRotate = 25f;

        private Transform _transform;
        private Quaternion _startRotation;

        private void Awake()
        {
            _transform = transform;
            _startRotation = _transform.rotation;
        }

        private void OnEnable()
        {
            _transform.localRotation = _startRotation;
        }

        public void LookAtTarget(Vector3 localTargetPosition)
        {
            localTargetPosition.x = 0f;

            if (localTargetPosition.sqrMagnitude < 0.0001f)
                return;

            float angle = -Mathf.Atan2(localTargetPosition.y, localTargetPosition.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(angle, 0f, 0f);

            _transform.localRotation = Quaternion.RotateTowards(
                _transform.localRotation,
                targetRotation,
                _speedRotate * Time.deltaTime
            );
        }
    }
}