using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Aim
{
    public class RadarAim : MonoBehaviour, IAim
    {
        private IAttackerPresenter _presenter;
        private IAttackNotifier _attackNotifier;
        private Transform _transform;
        private Quaternion _startRotation;

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

        private void OnTakeAim()
        {
            _presenter.EstablishAimState(true);
        }

        private void OnRemoveTarget()
        {
            _presenter.EstablishAimState(false);
        }
    }
}