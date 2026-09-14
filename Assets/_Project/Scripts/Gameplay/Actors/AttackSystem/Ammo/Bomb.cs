using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    [RequireComponent(typeof(LineMover), typeof(Exception))]
    public class Bomb : Projectile, IProjectileHitEvent
    {
        private Transform _transform;
        private IProjectileMover _mover;
        private IExplosion _explosion;
        private TeamType _enemyTeam;

        public override event Action<Projectile> Deactivated;
        public event Action Hited;

        private void Awake()
        {
            _transform = transform;
            _mover = GetComponent<IProjectileMover>();
            _explosion = GetComponent<IExplosion>();
        }

        private void FixedUpdate()
        {
            _mover.Move(Time.fixedDeltaTime);

            if (_mover.IsFinished)
            {
                _explosion.Explode(Damage, _mover.CurrentPosition, _enemyTeam);

                Hited?.Invoke();
                Deactivated?.Invoke(this);
            }
        }

        public override void ShootTarget(IShotPointTransform shotPointTransform, ITarget target)
        {
            if (shotPointTransform == null)
                throw new ArgumentNullException(nameof(shotPointTransform));

            if (target == null) 
                throw new ArgumentNullException(nameof(target));

            _enemyTeam = target.TeamType;

            Vector3 shotPosition = shotPointTransform.Position;
            Vector3 finishPosition = shotPointTransform.Position;
            finishPosition.y = 0;
            _mover.SetStartPosition(shotPosition);
            _mover.SetPointPosition(finishPosition);
            _mover.SetSpeed(Config.Speed);
            _transform.rotation = shotPointTransform.Rotation;
        }
    }
}