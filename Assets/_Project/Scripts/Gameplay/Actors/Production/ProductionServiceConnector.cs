using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production.Factories;
using BattleBase.Gameplay.Actors.Spawn;
using System;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionServiceConnector : IActorComponentConnector
    {
        public void Connect(IActor actor)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IActorSpawner spawner) == false)
                return;

            if (actor.TryGetComponent(out IProductionService productionService) == false)
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