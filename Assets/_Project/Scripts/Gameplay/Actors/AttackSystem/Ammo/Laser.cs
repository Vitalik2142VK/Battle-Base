using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(LaserView), typeof(InstantMover))]
    public class Laser : Projectile
    {
        [SerializeField][Range(0, 1f)] private float _lifeTime = 0.2f;

        private InstantMover _mover;
        private LaserView _laserView;
        private WaitForSeconds _wait;
        private ITarget _target;

        public override event Action<Projectile> Deactivated;

        private void Awake()
        {
            _mover = GetComponent<InstantMover>();
            _laserView = GetComponent<LaserView>();

            _wait = new WaitForSeconds(_lifeTime);
        }

        public override void ShootTarget(IShotPointTransform shotPointTransform, ITarget target)
        {
            if (_target != null)
                return;

            _target = target ?? throw new ArgumentNullException(nameof(target));
            _mover.SetStartPosition(shotPointTransform.Position);
            _mover.SetPointPosition(_target.Position);
            _mover.Move();

            TryHit();

            _laserView.Show(shotPointTransform.Position, _target.Position);

            StartCoroutine(WaitDeactivate());
        }

        private void TryHit()
        {
            if (_target.HasHit(_mover.CurrentPosition))
                _target.TakeDamage(Damage);
        }

        public IEnumerator WaitDeactivate()
        {
            yield return _wait;

            Deactivate();
        }

        private void Deactivate()
        {
            _target = null;

            Deactivated?.Invoke(this);
        }
    }
}