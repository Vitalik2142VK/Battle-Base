using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.AttackSystem;
using System;
using BattleBase.Gameplay.Actors.AttackSystem.Weapons;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class AttackToMoveStateTransitionFactory : IStateTransitionFactory
    {
        public StateTransitionType TransitionType => StateTransitionType.AttackToMove;

        public void Implement(IActor actor, IActorStateMachine actorStateMachine)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actorStateMachine == null)
                throw new ArgumentNullException(nameof(actorStateMachine));

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IWeapon)}");

            if (actor.TryGetComponent(out IMover mover) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IMover)}");

            MoveState moveState = new(mover);
            actorStateMachine.Init(moveState);
            AttackToMoveStateTransition transition = new(moveState, attacker);
            actorStateMachine.AddStateTransition(transition);
        }
    }
}