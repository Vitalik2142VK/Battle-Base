using BattleBase.Gameplay.Actors.AI.Transition;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AI
{
    public interface IStateMachineSource : IComponentSource
    {
        public IEnumerable<StateTransitionType> TransitionTypes { get; }
    }
}