using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileController : IProjectileController
    {
        private readonly IProjectileSpawner _spawner;
        private readonly IShotPoint _shotPoint;
        private readonly IProjectileConfig _projectileConfig;

        public ProjectileController(
            IProjectileSpawner spawner, 
            IShotPoint shotPoint, 
            IProjectileConfig projectileConfig)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _shotPoint = shotPoint ?? throw new ArgumentNullException(nameof(shotPoint));
            _projectileConfig = projectileConfig ?? throw new ArgumentNullException(nameof(projectileConfig));
        }

        public void ShootMissile(ITarget target, IDamage damage)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (damage == null)
                throw new ArgumentNullException(nameof(damage));

            IProjectile projectile = _spawner.Spawn(_projectileConfig.MissleId);
            projectile.SetProjectileConfig(_projectileConfig);
            projectile.SetDamage(damage);
            projectile.ShootTarget(_shotPoint.ShotPointTransform, target);
        }
    }
}