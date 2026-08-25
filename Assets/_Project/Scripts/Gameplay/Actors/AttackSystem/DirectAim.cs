using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class DirectAim : MonoBehaviour, IAimComponent
    {
        [SerializeField][Range(-1f, 1f)] private float _dotAim = 0.9f;

        private Transform _transform;

        public bool IsAimed { get; private set; }

        private void Awake()
        {
            _transform = transform;
        }

        public void LookAtTarget(Vector3 targetPosition, float delta)
        {
            if (delta < 0f)
                throw new System.ArgumentOutOfRangeException(nameof(delta));

            Vector3 direction = targetPosition - _transform.position;
            float dot = Vector3.Dot(_transform.forward, direction.normalized);

            IsAimed = dot > _dotAim;
        }
    }
}