using System;

namespace BattleBase.Gameplay.Actors.Colored
{
    public class ColoredActorBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (view.TryGetViewComponent(out IColoredActorView coloredView))
                coloredView.Init(actor);
        }
    }
}
