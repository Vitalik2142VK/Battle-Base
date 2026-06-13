using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerBinder : IActorComponentBinder
    {
        private readonly IProjectileSpawner _projectileSpawner;

        public AttackerBinder(IProjectileSpawner projectileSpawner)
        {
            _projectileSpawner = projectileSpawner ?? throw new ArgumentNullException(nameof(projectileSpawner));
        }

        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IAttacker attacker) &&
                view.TryGetViewComponent(out IAttackerViewComponent weaponView))
            {
                if (view.TryGetViewComponent(out IShotPoint shotPoint) == false)
                    throw new InvalidOperationException($"'{nameof(view)}' don't contain module '{nameof(IShotPoint)}'");

                IProjectileConfig projectileConfig = attacker.WeaponConfig.ProjectileConfig;
                TargetController targetController = new(view, attacker.WeaponConfig);
                ProjectileController projectileController = new(_projectileSpawner, shotPoint, projectileConfig);

                attacker.Init(targetController, projectileController);
                weaponView.Init(attacker);
            }
            else
            {
                return;
            }

            AttackerPresenter presenter = new(attacker);

            if (view.TryGetViewComponent(out IAim aim))
                aim.Init(presenter, attacker);

            if (view.TryGetViewComponent(out ITargetFinder targetFinder))
                targetFinder.Init(presenter, attacker.WeaponConfig, actor);
        }
    }
}