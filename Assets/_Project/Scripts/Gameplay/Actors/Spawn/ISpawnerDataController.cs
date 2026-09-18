using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface ISpawnerDataController : IActorDataStorage
    {
        public IEnumerable<IActorData> AvailabilityActorDatas { get; }

        public bool ConstrainActorData(IActorData actorData);

        public void EstablishActors(IEnumerable<IActorData> actorDatas);
    }
}