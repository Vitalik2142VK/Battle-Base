using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public interface IEnemyAvailableActorProvider
    {
        public IEnumerable<IActorConfig> ActualConfigs { get; }
    }
}