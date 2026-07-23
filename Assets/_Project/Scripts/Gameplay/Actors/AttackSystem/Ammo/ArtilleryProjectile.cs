using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(ParabolaMover), typeof(Explosion))]
    public class ArtilleryProjectile : Projectile
    {
        [SerializeField][Range(0f, 10f)] private float _randomOffset = 5f;

        private IProjectileMover _mover;
        private ITarget _target;
        private IExplosion _explosion;

        public override event Action<Projectile> Deactivated;

        private void Awake()
        {
            _mover = GetComponent<IProjectileMover>();
            _explosion = GetComponent<IExplosion>();
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);

            if (_mover.IsFinished)
                Deactivate();
        }

        private void OnDisable()
        {
            OnTargetLost();
        }

        public override void ShootTarget(IShotPointTransform shotPointTransform, ITarget target)
        {
            if (shotPointTransform == null)
                throw new ArgumentNullException(nameof(shotPointTransform));

            if (_target != null)
                return;

            _target = target ?? throw new ArgumentNullException(nameof(target));

            Vector3 randomPosition = GetRandomPosition(target.Position);

            _mover.SetStartPosition(shotPointTransform.Position);
            _mover.SetPointPosition(randomPosition);
            _mover.SetSpeed(Config.Speed);

            _target.Destroyed += OnTargetLost;
        }

        private void Deactivate()
        {
            _explosion.Explode(Damage, _mover.CurrentPosition);

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

        private Vector3 GetRandomPosition(Vector3 targetPosition)
        {
            float randomX = GetRandomOffset(targetPosition.x);
            float randomZ = GetRandomOffset(targetPosition.z);

            return new Vector3(randomX, 0, randomZ);
        }

        private float GetRandomOffset(float target) =>
            UnityEngine.Random.Range(target - _randomOffset, target + _randomOffset);
    }
}