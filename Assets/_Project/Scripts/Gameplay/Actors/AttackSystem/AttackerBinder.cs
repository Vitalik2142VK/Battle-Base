using BattleBase.Gameplay.Actors.AttackSystem.Aim;
using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using System;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerBinder : IActorComponentBinder
    {
        private readonly AttackerInitializer _attackerInitializer;

        public AttackerBinder(AttackerInitializer attackerInitializer)
        {
            _attackerInitializer = attackerInitializer ?? throw new ArgumentNullException(nameof(attackerInitializer));
        }

        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                return;

            if (view.TryGetViewComponent(out IShotPoint shotPoint) == false)
                throw new InvalidOperationException($"'{nameof(view)}' don't contain module '{nameof(IShotPoint)}'");

            _attackerInitializer.Init(attacker, shotPoint, actor.Position);

            if (view.TryGetViewComponent(out IAttackerViewComponent weaponView))
                weaponView.Init(attacker);

            AttackerPresenter presenter = new(attacker);

            if (view.TryGetViewComponent(out IAim aim))
                aim.Init(presenter, attacker);

            if (view.TryGetViewComponent(out ITargetFinder targetFinder))
                targetFinder.Init(presenter, attacker.TargetFinderConfig, actor);
        }
    }
}