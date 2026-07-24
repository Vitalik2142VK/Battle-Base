using BattleBase.Core;
using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production.Improve;
using BattleBase.Gameplay.Actors.Production.Spawn;
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

        public IEnumerable<IProductionOption> Create()
        {
            List<IProductionOption> productionOptions = new();

            foreach (var spawnData in _improver.SpawnDatas)
            {
                IActorData actorData = spawnData.ActorData;
                SpawnCommand spawnCommand = new(_spawner, actorData);
                CancelSpawnCommand cancelSpawnCommand = new(_spawner, actorData);
                IProductionOption productionOption = new SpawnProductionOption(
                    spawnCommand,
                    cancelSpawnCommand,
                    spawnData);
                productionOptions.Add(productionOption);
            }

            if (_improver.CanImprove)
            {
                DelegateCommand command = new(() => _improver.TryImprove());
                ProductionOption productionOption = new(command, _improver.Data, TypeProduction.Improve);
                IImproveProductionData data = _improver.Data;
                ImproveProductionOption improveProductionOption = new(
                    productionOption,
                    data.MaterialData,
                    data);
                productionOptions.Add(improveProductionOption);
            }

            return productionOptions;
        }
    }
}