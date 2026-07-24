using BattleBase.Gameplay.Actors.AI.Transition;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.AI
{
    public class ActorStateMachine : IActorStateMachine
    {
        private readonly List<IStateTransition> _transitions;

        private IActorState _currentState;

        public ActorStateMachine(IEnumerable<StateTransitionType> transitionTypes)
        {
            _transitions = new List<IStateTransition>();

            TransitionTypes = transitionTypes ?? throw new ArgumentNullException(nameof(transitionTypes));
        }

        public Type KeyType => typeof(IActorStateMachine);

        public IEnumerable<StateTransitionType> TransitionTypes { get; }

        public void Init(IActorState defaultState)
        {
            _currentState ??= defaultState ?? throw new ArgumentNullException(nameof(defaultState));
        }

        public void Enable()
        {
            foreach (var transition in _transitions)
            {
                transition.StateChanged += OnChangeState;
                transition.Enable();
            }

            _currentState.Enter();
        }

        public void Disable()
        {
            foreach (var transition in _transitions)
            {
                transition.StateChanged -= OnChangeState;
                transition.Disable();
            }

            _currentState.Exit();
        }

        public void Update(float delta)
        {
            _currentState.Update(delta);
        }

        public void AddStateTransition(IStateTransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition));

            _transitions.Add(transition);
        }

        private void OnChangeState(IActorState state)
        {
            if (state == null)
                throw new ArgumentException(nameof(state));

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}