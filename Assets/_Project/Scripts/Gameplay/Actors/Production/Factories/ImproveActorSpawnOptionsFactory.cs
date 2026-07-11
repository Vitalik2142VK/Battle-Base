using BattleBase.Core;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production.Factories
{
    public class ImproveActorSpawnOptionsFactory : IProductionOptionsFactory
    {
        private readonly IActorSpawner _spawner;
        private readonly ISpawnerImprover _improver;

        public ImproveActorSpawnOptionsFactory(IActorSpawner spawner, ISpawnerImprover improvement)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
        }

        public IEnumerable<ProductionOption> Create()
        {
            List<ProductionOption> productionOptions = new();

            foreach (var actorData in _improver.ActorDatas)
            {
                SpawnCommand spawnCommand = new(_spawner, actorData);
                ProductionOption productionOption = new(spawnCommand, actorData, TypeProduction.Spawn);
                productionOptions.Add(productionOption);
            }

            if (_improver.CanImprove)
            {
                DelegateCommand command = new(() => _improver.Improve());
                ProductionOption improveProductionOption = new(command, _improver.Data, TypeProduction.Improve);
                productionOptions.Add(improveProductionOption);
            }

            return productionOptions;
        }
    }
}