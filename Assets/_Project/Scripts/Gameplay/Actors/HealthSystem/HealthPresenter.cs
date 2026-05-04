using System;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthPresenter : IActorComponentPresenter
    {
        private readonly IHealth _health;
        private readonly IHealthViewComponent _view;

        public HealthPresenter(IHealth health, IHealthViewComponent view)
        {
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.Init(_health);
        }
    }
}