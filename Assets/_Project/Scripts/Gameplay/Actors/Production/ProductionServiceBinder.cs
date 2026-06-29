using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Spawn;
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

            TryAddCreateActorOptionsFactory(actor, productionService);

            if (view.TryGetViewComponent(out IProductionView productionView))
            {
                ProductionPresenter presenter = new(productionService);
                productionView.Init(presenter, actor);
            }
        }

        private void TryAddCreateActorOptionsFactory(IActor actor, IProductionService productionService)
        {
            if (actor.TryGetComponent(out IActorSpawner spawner) == false)
                return;

            IProductionOptionsFactory factory;

            if (actor.TryGetComponent(out ISpawnerImprovement spawnerImprovement))
                factory = new ImproveActorSpawnOptionsFactory(spawner, spawnerImprovement);
            else
                factory = new ActorSpawnOptionsFactory(spawner);

            productionService.AddProductionFactory(factory);
        }
    }
}