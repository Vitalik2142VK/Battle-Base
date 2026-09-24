using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public interface IPlayerAvailableActorProvider
    {
        public IEnumerable<IActorConfig> ActualConfigs { get; }
    }
}