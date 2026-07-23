using BattleBase.Gameplay.Actors.DamageSystem;
using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IHealth health) == false)
                return;

            if (view.TryGetViewComponent(out IHealthViewComponent healthView))
                healthView.Init(health);

            IDestroyableEvent destroyableEvent;

            if (actor.TryGetComponent(out IDestroyComponent destroyComponent))
                destroyableEvent = destroyComponent;
            else
                destroyableEvent = health;

            if (view.TryGetViewComponent(out ITarget target))
            {
                HealthPresenter presenter = new(health);
                target.Init(presenter, destroyableEvent, actor, health.ActorMask);
            }
        }
    }
}