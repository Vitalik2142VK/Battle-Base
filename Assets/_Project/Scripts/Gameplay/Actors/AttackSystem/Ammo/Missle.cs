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

        public override event Action<Projectile> Deactivated;

        private void Awake()
        {
            _mover = GetComponent<IProjectileMover>();
        }

        private void FixedUpdate()
        {
            if (_target == null)
                return;

            _mover.SetPointPosition(_target.Position);
            _mover.Move(Time.fixedDeltaTime);

            if (HasHit())
                return;

            if (_mover.IsFinished)
                Deactivate();
        }

        public override void ShootTarget(Vector3 startPosition, ITarget target)
        {
            if (_target != null)
                return;

            _target = target ?? throw new ArgumentNullException(nameof(target));
            _mover.SetStartPosition(startPosition);
            _mover.SetPointPosition(_target.Position);
            _mover.SetSpeed(Config.Speed);
        }

        private bool HasHit()
        {
            if (_target.HasHit(_mover.CurrentPosition) == false)
                return false;

            _target.TakeDamage(Damage);

            Deactivate();

            return true;
        }

        private void Deactivate()
        {
            _target = null;

            Deactivated?.Invoke(this);
        }
    }
}