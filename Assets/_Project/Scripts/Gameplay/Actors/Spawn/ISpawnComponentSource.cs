using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public interface ISpawnComponentSource : IComponentSource
    {
        public IEnumerable<IActorConfig> ActorsConfigs { get; }
    }
}