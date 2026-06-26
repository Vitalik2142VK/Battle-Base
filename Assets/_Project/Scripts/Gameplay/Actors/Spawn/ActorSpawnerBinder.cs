using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerBinder : IActorComponentBinder
    {
        private readonly ProductionServiceFactory _factory;

        public ActorSpawnerBinder(ProductionServiceFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IActorSpawner spawner) == false)
                return;

            if (view.TryGetViewComponent(out ISpawnPoint spawnPoint) == false)
                throw new InvalidOperationException($"'{nameof(view)}' don't contain module '{nameof(ISpawnPoint)}'");

            spawner.Init(actor, spawnPoint);

            if (view.TryGetViewComponent(out IActorSpawnerView spawnerView))
                spawnerView.Init(spawner);

            if (actor.TryGetComponent(out IProductionService productionService) == false)
                productionService = _factory.Create(actor, view);

            productionService.AddProductionStorage(spawner);
        }
    }
}