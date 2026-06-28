using System;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class MoverBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IMover mover) == false)
                return;

            if (view.TryGetViewComponent(out IMoverViewComponent moverView))
                moverView.Init(mover);

            if (view.TryGetViewComponent(out INavigationAgent agent))
            {
                MoverPresenter presenter = new(mover);
                agent.Init(presenter, mover.Config, mover);
            }
        }
    }
}
