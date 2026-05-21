using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorPoolsRegistrator
    {
        public IDictionary<string, ActorPool> Pools { get; }
    }
}