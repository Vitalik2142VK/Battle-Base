using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorConfig
    {
        public IActorData Data { get; }

        public IEnumerable<IComponentSource> GetComponentSources();
    }

    public class ActorInfo : IActorConfig
    {
        public ActorInfo(IActorData data)
        {
            Data = data;
        }

        public IActorData Data { get; }

        public IEnumerable<IComponentSource> GetComponentSources()
        {
            throw new System.NotImplementedException();
        }
    }
}
