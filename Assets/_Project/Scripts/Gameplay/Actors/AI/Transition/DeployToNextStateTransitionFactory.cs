using BattleBase.Gameplay.Actors.AI.Activated;
using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.AttackSystem;
using BattleBase.Gameplay.Actors.DeploymentSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Movement.Hunt;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class DeployToNextStateTransitionFactory : IStateTransitionFactory
    {
        public StateTransitionType TransitionType => StateTransitionType.DepoyToMove;

        public void Implement(IActor actor, IActorStateMachine actorStateMachine)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (actorStateMachine == null)
                throw new ArgumentNullException(nameof(actorStateMachine));

            if (actor.TryGetComponent(out IDeployment deployment) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IDeployment)}");

            if (actor.TryGetComponent(out IMover mover) == false)
                throw new InvalidOperationException($"{nameof(actor)} don't constrain component {nameof(IMover)}");

            DeploymentState deploymentState = new(deployment);
            actorStateMachine.Init(deploymentState);
            MoveState moveState = new(mover);
            DeployToNextStateTransition transition = new(moveState, deployment);
            actorStateMachine.AddStateTransition(transition);

            if (actor.TryGetComponent(out IAttacker attacker) == false)
                return;

            IActorState actorState;

            if (actor.TryGetComponent(out IHuntMover huntMover))
                actorState = new HunterState(attacker, huntMover);
            else
                actorState = new AttackState(attacker);

            AttackActivatedState attackActivatedState = new(attacker, actorState);
            transition.SetActivatedState(attackActivatedState);
        }
    }
}