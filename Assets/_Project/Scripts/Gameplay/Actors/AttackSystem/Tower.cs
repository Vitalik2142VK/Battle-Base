using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class Tower : MonoBehaviour, IAim
    {
        private const float DotAim = 0.99f;

        [SerializeField] private Muzzle _muzzle;
        [SerializeField][Min(1f)] private float _speedRotate = 25f;

        private IAttackerPresenter _presenter;
        private IAttackNotifier _attackNotifier;
        private ITargetPoint _currentTarget;
        private Transform _transform;
        private Quaternion _startRotation;
        private bool _isAimed;

        private void Awake()
        {
            _transform = transform;
            _startRotation = _transform.localRotation;
        }

        private void OnEnable()
        {
            if (_attackNotifier != null)
            {
                _attackNotifier.TargetSelected += OnTakeAim;
                _attackNotifier.AttackDeactivated += OnRemoveTarget;

                OnTakeAim();
            }
            
            _transform.localRotation = _startRotation;
        }

        private void Update()
        {
            if (_currentTarget != null)
                LookAtTarget();
        }

        private void OnDisable()
        {
            OnRemoveTarget();

            if (_attackNotifier != null)
            {
                _attackNotifier.TargetSelected -= OnTakeAim;
                _attackNotifier.AttackDeactivated -= OnRemoveTarget;
            }
        }

        public void Init(IAttackerPresenter presenter, IAttackNotifier weaponEvents)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _attackNotifier = weaponEvents ?? throw new ArgumentNullException(nameof(weaponEvents));

            if (gameObject.activeSelf)
            {
                _attackNotifier.TargetSelected += OnTakeAim;
                _attackNotifier.AttackDeactivated += OnRemoveTarget;
            }
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
                _presenter.EstablishAimState(_isAimed);
            }
        }

        private void OnTakeAim()
        {
            _currentTarget = _attackNotifier.CurrentTarget;
            _isAimed = false;
        }

        private void OnRemoveTarget()
        {
            _currentTarget = null;
            _isAimed = false;
            _presenter.EstablishAimState(_isAimed);
        }
    }
}