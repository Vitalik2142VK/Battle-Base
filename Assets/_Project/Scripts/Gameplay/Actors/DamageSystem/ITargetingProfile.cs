using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public interface ITargetingProfile
    {
        public ActorMask NotAttacked { get; }

        public IEnumerable<IPriorityActorType> PriorityActorTypes { get; }
    }
}
