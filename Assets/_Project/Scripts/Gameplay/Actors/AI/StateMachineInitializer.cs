using BattleBase.Gameplay.Actors.AI.Transition;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AI
{
    public class StateMachineInitializer : IStateMachineInitializer
    {
        private readonly Dictionary<StateTransitionType, IStateTransitionFactory> _factories;

        public StateMachineInitializer(IEnumerable<IStateTransitionFactory> factories)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories));

            _factories = new Dictionary<StateTransitionType, IStateTransitionFactory>();

            foreach (var factory in factories)
                _factories.Add(factory.TransitionType, factory);
        }

        public void Initialize(IActor actor)
        {
            if (actor == null) 
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IActorStateMachine actorStateMachine) == false)
                return;

            foreach (var transitionType in actorStateMachine.TransitionTypes)
                _factories[transitionType].Implement(actor, actorStateMachine);
        }
    }
}