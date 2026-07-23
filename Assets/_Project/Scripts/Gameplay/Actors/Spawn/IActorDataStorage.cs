using BattleBase.Gameplay.Actors.Production.Spawn;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorDataStorage
    {
        public IEnumerable<ISpawnProductionData> SpawnDatas { get; }
    }
}