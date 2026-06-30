using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class ActorSpawnOptionsFactory : IProductionOptionsFactory
    {
        private readonly IActorSpawner _spawner;

        public ActorSpawnOptionsFactory(IActorSpawner spawner)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
        }

        public IEnumerable<ProductionOption> Create()
        {
            List<ProductionOption> result = new();

            foreach (var actorData in _spawner.ActorDatas)
            {
                SpawnCommand spawnCommand = new(_spawner, actorData);
                ProductionOption productionOption = new(spawnCommand, actorData);
                result.Add(productionOption);
            }

            return result;
        }
    }
}