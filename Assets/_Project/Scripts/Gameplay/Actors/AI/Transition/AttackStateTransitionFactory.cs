using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class AttackStateTransitionFactory : IStateTransitionFactory
    {
        public StateTransitionType TransitionType => StateTransitionType.Attack;

        public void Implement(IActor actor, IActorStateMachine actorStateMachine)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IWeapon)}");

            if (actorStateMachine == null)
                throw new ArgumentNullException(nameof(actorStateMachine));

            AttackState attackState = new(attacker);
            AttackStateTransition transition = new(attackState, attacker);
            actorStateMachine.AddStateTransition(transition);
        }
    }
}