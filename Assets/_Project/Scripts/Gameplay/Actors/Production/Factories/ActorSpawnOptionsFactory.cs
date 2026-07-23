using BattleBase.Gameplay.Actors.Production.Spawn;
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

        public IEnumerable<IProductionOption> Create()
        {
            List<IProductionOption> result = new();

            foreach (var spawnData in _spawner.SpawnDatas)
            {
                IActorData actorData = spawnData.ActorData;
                SpawnCommand spawnCommand = new(_spawner, actorData);
                CancelSpawnCommand cancelSpawnCommand = new(_spawner, actorData);
                IProductionOption productionOption = new SpawnProductionOption(
                    spawnCommand,
                    cancelSpawnCommand,
                    spawnData);
                result.Add(productionOption);
            }

            return result;
        }
    }
}