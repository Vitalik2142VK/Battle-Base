using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerInitializer
    {
        private readonly IProjectileSpawner _projectileSpawner;

        public AttackerInitializer(IProjectileSpawner projectileSpawner)
        {
            _projectileSpawner = projectileSpawner ?? throw new ArgumentNullException(nameof(projectileSpawner));
        }

        public void Init(IAttacker attacker, IShotPoint shotPoint, IActorPosition actorPosition)
        {
            IWeaponConfig weaponConfig = attacker.WeaponConfig;
            ITargetingProfile targetingProfile = weaponConfig.DamageConfig.TargetingProfile;
            IProjectileConfig projectileConfig = weaponConfig.ProjectileConfig;
            TargetController targetController = new(actorPosition, attacker.WeaponConfig, targetingProfile);
            ProjectileController projectileController = new(_projectileSpawner, shotPoint, projectileConfig);

            attacker.Init(targetController, projectileController);
        }
    }
}