using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorDataStorage
    {
        public IEnumerable<IActorData> ActorDatas { get; }
    }
}