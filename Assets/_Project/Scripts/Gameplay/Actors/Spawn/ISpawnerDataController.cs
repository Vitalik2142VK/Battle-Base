using BattleBase.Gameplay.Actors.Production.Spawn;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface ISpawnerDataController
    {
        public IEnumerable<IActorData> AvailabilityActorDatas { get; }

        public IEnumerable<ISpawnProductionDataByTier> SpawnProductionDatas { get; }

        public bool ConstrainActorData(IActorData actorData);

        public void EstablishActors(IEnumerable<IActorData> actorDatas);
    }
}