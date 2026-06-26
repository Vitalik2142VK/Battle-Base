namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionServiceFactory
    {
        public ProductionService Create(IActor actor, IActorView actorView)
        {
            if (actor == null) 
                throw new System.ArgumentNullException(nameof(actor));

            if (actorView == null)
                throw new System.ArgumentNullException(nameof(actorView));

            ProductionService productionService = new();
            actor.AddComponent(productionService);

            if (actorView.TryGetViewComponent(out IProductionView productionView))
            {
                ProductionPresenter presenter = new(productionService);
                productionView.Init(presenter, actor);
            }

            return productionService;
        }
    }
}