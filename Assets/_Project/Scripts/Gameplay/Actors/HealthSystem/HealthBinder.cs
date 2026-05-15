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

            if (actor.TryGetComponent(out IHealth health) &&
                view.TryGetViewComponent(out IHealthViewComponent healthView))
            {
                healthView.Init(health);
            }

            HealthPresenter presenter = new(health);

            UnityEngine.Debug.Log($"HealthBinder (view.TryGetViewComponent(out ITarget target) == {view.TryGetViewComponent(out ITarget _)}");

            if (view.TryGetViewComponent(out ITarget target))
            {
                target.Init(presenter, health, actor.Data);
            }
        }
    }
}