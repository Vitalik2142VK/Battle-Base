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

            UnityEngine.Debug.Log($"Components: actor.IHealth == {actor.TryGetComponent(out IHealth _)} || view.IHealthViewComponent ==  {view.TryGetViewComponent(out IHealthViewComponent _)}");

            if (actor.TryGetComponent(out IHealth health) && 
                view.TryGetViewComponent(out IHealthViewComponent healthView))
            {
                healthView.Init(health);
            }

            if (view.TryGetViewComponent(out ITarget target))
            {
                HealthPresenter healthPresenter = new(health);
                target.Init(healthPresenter, health);
            }
        }
    }
}