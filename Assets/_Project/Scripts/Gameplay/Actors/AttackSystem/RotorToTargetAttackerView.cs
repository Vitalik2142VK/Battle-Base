using BattleBase.Gameplay.Actors.DamageSystem;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class RotorToTargetAttackerView : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField] private ActorView _actorView;
        [SerializeField][Range(0.1f, 1)] private float _dotAim = 0.9f;
        [SerializeField][Range(1f, 720f)] private float _speedRotate = 180f;

        private IAttackNotifier _attackNotifier;
        private ITarget _currentTarget;
        private Transform _transformActorView;

        private void Awake()
        {
            _transformActorView = _actorView.transform;
        }

        private void OnEnable()
        {
            if (_attackNotifier != null)
            {
                _attackNotifier.Attacked += OnAimTarget;
                _attackNotifier.AttackDeactivated += OnLostTarget;

                OnAimTarget();
            }
        }

        private void Update()
        {
            if (_currentTarget == null)
                return;

            LookAtTarget();
        }

        private void OnDisable()
        {
            _attackNotifier.Attacked -= OnAimTarget;
            _attackNotifier.AttackDeactivated -= OnLostTarget;

            OnLostTarget();
        }

        public void Init(IAttackNotifier attackNotifier)
        {
            _attackNotifier = attackNotifier ?? throw new System.ArgumentNullException(nameof(attackNotifier));

            if (gameObject.activeSelf)
            {
                _attackNotifier.Attacked += OnAimTarget;
                _attackNotifier.AttackDeactivated += OnLostTarget;
            }
        }

        private void LookAtTarget()
        {
            Vector3 direction = _currentTarget.Position - _transformActorView.position;
            direction.y = 0f;
            float dot = Vector3.Dot(_transformActorView.forward, direction.normalized);

            if (dot > _dotAim)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _transformActorView.rotation = Quaternion.RotateTowards(
                _transformActorView.rotation,
                targetRotation,
                _speedRotate * Time.deltaTime
            );

            Vector3 localTargetPosition = _transformActorView.InverseTransformPoint(_currentTarget.Position);
        }

        private void OnAimTarget()
        {
            _currentTarget = _attackNotifier.CurrentTarget;
        }

        private void OnLostTarget()
        {
            _currentTarget = null;
        }
    }
}