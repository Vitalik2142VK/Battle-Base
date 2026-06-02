using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem.Missiles
{
    public class MissileController : IMissileController
    {
        private readonly IMissileSpawner _spawner;
        private readonly IShotPoint _shotPoint;
        private readonly IDamageConfig _damageConfig;

        public MissileController(IMissileSpawner spawner, IShotPoint shotPoint, IDamageConfig damageConfig)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _shotPoint = shotPoint ?? throw new ArgumentNullException(nameof(shotPoint));
            _damageConfig = damageConfig ?? throw new ArgumentNullException(nameof(damageConfig));
        }

        public void ShootMissile(ITarget target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            IMissile missile = _spawner.Spawn(_damageConfig.MissleId);
            Damage damage = new(_damageConfig);
            missile.SetDamage(damage);
            missile.ShootTarget(_shotPoint.Position, target);
        }
    }
}