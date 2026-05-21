using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorConfig
    {
        public IActorData Data { get; }

        public IEnumerable<IComponentSource> GetComponentSources();
    }
}
