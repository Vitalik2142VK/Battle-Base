using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(HomingMover))]
    public class Missle : Projectile
    {
        private IProjectileMover _mover;
        private ITarget _target;
        private Vector3 _lastTargetPosition;

        public override event Action<Projectile> Deactivated;

        private void Awake()
        {
            _mover = GetComponent<IProjectileMover>();
        }

        private void FixedUpdate()
        {
            if (_target != null)
            {
                _lastTargetPosition = _target.Position;
                _mover.SetPointPosition(_lastTargetPosition);
            }
            
            _mover.Move(Time.fixedDeltaTime);

            if (HasHit())
                return;

            if (_mover.IsFinished)
                Deactivate();
        }

        private void OnDisable()
        {
            OnTargetLost();
        }

        public override void ShootTarget(Vector3 startPosition, ITarget target)
        {
            if (_target != null)
                return;

            _target = target ?? throw new ArgumentNullException(nameof(target));
            _lastTargetPosition = _target.Position;
            _mover.SetStartPosition(startPosition);
            _mover.SetPointPosition(_lastTargetPosition);
            _mover.SetSpeed(Config.Speed);

            _target.Destroyed += OnTargetLost;
        }

        private bool HasHit()
        {
            if (_target == null || _target.HasHit(_mover.CurrentPosition) == false)
                return false;

            _target.TakeDamage(Damage);

            Deactivate();

            return true;
        }

        private void Deactivate()
        {
            OnTargetLost();

            Deactivated?.Invoke(this);
        }

        private void OnTargetLost()
        {
            if (_target == null)
                return;

            _target.Destroyed -= OnTargetLost;
            _target = null;
        }
    }
}