using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface IActorSpawnerSource : IComponentSource
    {
        public IEnumerable<IActorConfig> ActorsConfigs { get; }
    }
}