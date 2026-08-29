using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class CombinedAim : MonoBehaviour, IAim
    {
        [SerializeField][SerializeIterface(typeof(IAimComponent))] private GameObject[] _aimComponents;

        private List<IAimComponent> _components;
        private IAttackerPresenter _presenter;
        private IAttackNotifier _attackNotifier;
        private ITargetPoint _currentTarget;
        private bool _isAimed;
        private bool _isReturning;

        private void OnEnable()
        {
            if (_attackNotifier != null)
            {
                _attackNotifier.TargetSelected += OnTakeAim;
                _attackNotifier.AttackDeactivated += OnRemoveTarget;

                OnTakeAim();
            }
        }

        private void Update()
        {
            if (_currentTarget != null)
            {
                AimAtTarget(Time.deltaTime);

                return;
            }

            if (_isReturning)
                ReturnToStart(Time.deltaTime);
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

        public void Init(IAttackerPresenter presenter, IAttackNotifier attackNotifier)
        {
            _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _attackNotifier = attackNotifier ?? throw new ArgumentNullException(nameof(attackNotifier));

            if (gameObject.activeSelf)
            {
                _attackNotifier.TargetSelected += OnTakeAim;
                _attackNotifier.AttackDeactivated += OnRemoveTarget;
            }

            _components = new List<IAimComponent>();

            foreach (var gameObject in _aimComponents)
            {
                IAimComponent component = gameObject.GetComponent<IAimComponent>();
                _components.Add(component);
            }
        }

        private void AimAtTarget(float delta)
        {
            foreach (var aim in _components)
                aim.LookAtTarget(_currentTarget.Position, delta);

            bool isAimed = IsAimed();

            if (_isAimed == isAimed)
                return;

            _isAimed = isAimed;
            _presenter.EstablishAimState(_isAimed);
        }

        private void ReturnToStart(float delta)
        {
            foreach (var aim in _components)
                aim.ReturnToStart(delta);

            if (AreAllRestored())
                _isReturning = false;
        }

        private void OnTakeAim()
        {
            _currentTarget = _attackNotifier.CurrentTarget;
            _isAimed = false;
            _isReturning = false;
        }

        private void OnRemoveTarget()
        {
            _currentTarget = null;
            _isAimed = false;
            _isReturning = AreAllRestored() == false;

            _presenter.EstablishAimState(_isAimed);
        }

        private bool IsAimed()
        {
            foreach (var aim in _components)
            {
                if (aim.IsAimed == false)
                    return false;
            }

            return true;
        }

        private bool AreAllRestored()
        {
            foreach (var aim in _components)
            {
                if (aim.IsRestored == false)
                    return false;
            }

            return true;
        }
    }
}