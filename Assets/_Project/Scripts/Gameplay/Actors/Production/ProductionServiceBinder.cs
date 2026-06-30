using System;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionServiceBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IProductionService productionService) == false)
                return;

            if (view.TryGetViewComponent(out IProductionView productionView))
            {
                ProductionPresenter presenter = new(productionService);
                productionView.Init(presenter, actor);
            }
        }
    }
}