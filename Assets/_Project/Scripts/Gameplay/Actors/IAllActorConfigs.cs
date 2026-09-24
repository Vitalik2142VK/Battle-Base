using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public interface IAllActorConfigs
    {
        public IEnumerable<IActorConfig> Configs { get; }
    }
}