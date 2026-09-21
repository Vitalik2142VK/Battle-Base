using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Availability
{
    public interface IAvailableActorConfigProvider
    {
        public IEnumerable<IActorConfig> ActualPlayerConfigs { get; }

        public IEnumerable<IActorConfig> ActualEnemyConfigs { get; }
    }
}