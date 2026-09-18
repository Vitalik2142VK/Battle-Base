using System.Collections.Generic;
using BattleBase.Gameplay.Actors;

namespace BattleBase
{
    public interface IAvailableActorConfigProvider
    {
        public IEnumerable<IActorConfig> ActualPlayerConfigs { get; }

        public IEnumerable<IActorConfig> ActualEnemyConfigs { get; }
    }
}