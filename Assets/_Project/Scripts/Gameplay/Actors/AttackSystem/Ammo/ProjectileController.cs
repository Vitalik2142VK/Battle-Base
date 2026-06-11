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
        private readonly IDamageConfig _damageConfig;

        public ProjectileController(
            IProjectileSpawner spawner, 
            IShotPoint shotPoint, 
            IProjectileConfig projectileConfig,
            IDamageConfig damageConfig)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _shotPoint = shotPoint ?? throw new ArgumentNullException(nameof(shotPoint));
            _projectileConfig = projectileConfig ?? throw new ArgumentNullException(nameof(projectileConfig));
            _damageConfig = damageConfig ?? throw new ArgumentNullException(nameof(damageConfig));
        }

        public void ShootMissile(ITarget target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            IProjectile projectile = _spawner.Spawn(_projectileConfig.MissleId);
            Damage damage = new(_damageConfig);
            projectile.SetProjectileConfig(_projectileConfig);
            projectile.SetDamage(damage);
            projectile.ShootTarget(_shotPoint.Position, target);
        }
    }
}