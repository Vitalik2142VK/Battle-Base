using BattleBase.Gameplay.Actors.AttackSystem.Missiles;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerBinder : IActorComponentBinder
    {
        private readonly IMissileSpawner _missileSpawner;

        public AttackerBinder(IMissileSpawner missileSpawner)
        {
            _missileSpawner = missileSpawner ?? throw new ArgumentNullException(nameof(missileSpawner));
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

                IDamageConfig damageConfig = attacker.WeaponConfig.DamageConfig;
                TargetController targetController = new(view, attacker.WeaponConfig);
                MissileController missileController = new(_missileSpawner, shotPoint, damageConfig);

                attacker.Init(targetController, missileController);
                weaponView.Init(attacker);
            }
            else
            {
                return;
            }

            AttackerPresenter presenter = new(attacker);


            if (view.TryGetViewComponent(out IAim aim))
            {
                aim.Init(presenter, attacker);
            }

            if (view.TryGetViewComponent(out ITargetFinder targetFinder))
            {
                targetFinder.Init(presenter, attacker.WeaponConfig, actor);
            }
        }
    }
}