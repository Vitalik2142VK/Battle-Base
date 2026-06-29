using BattleBase.Gameplay.Actors.AI.Transition;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AI
{
    public interface IActorStateMachine : IActorComponent, IUpdateable
    {
        public IEnumerable<StateTransitionType> TransitionTypes { get; }

        public void Init(IActorState defaultState);

        public void AddStateTransition(IStateTransition transition);
    }
}