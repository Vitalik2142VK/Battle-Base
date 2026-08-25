using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.Movement.Hunt;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class HunterStateTransitionFactory : IStateTransitionFactory
    {
        public StateTransitionType TransitionType => StateTransitionType.Hunter;

        public void Implement(IActor actor, IActorStateMachine actorStateMachine)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actorStateMachine == null)
                throw new ArgumentNullException(nameof(actorStateMachine));

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IAttacker)}");

            if (actor.TryGetComponent(out IHuntMover huntMover) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IHuntMover)}");

            HunterState hunterState = new(attacker, huntMover);
            HunterStateTransition transition = new(hunterState, attacker);
            actorStateMachine.AddStateTransition(transition);
        }
    }
}