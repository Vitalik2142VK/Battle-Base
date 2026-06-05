using BattleBase.Gameplay.Actors.AI.Transition;

namespace BattleBase.Gameplay.Actors.AI
{
    public interface IStateTransitionFactory
    {
        public StateTransitionType TransitionType { get; }

        public void Implement(IActor actor, IActorStateMachine actorStateMachine);
    }
}