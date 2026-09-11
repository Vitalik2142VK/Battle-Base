using BattleBase.Gameplay.Actors.AI.State;
using BattleBase.Gameplay.Actors.DeploymentSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class DeployToMoveStateTransition : IStateTransition
    {
        private readonly MoveState _moveState;
        private readonly IDeploymentEvets _deploymentEvets;

        public event Action<IActorState> StateChanged;

        public DeployToMoveStateTransition(MoveState moveState, IDeploymentEvets deploymentEvets)
        {
            _moveState = moveState ?? throw new ArgumentNullException(nameof(moveState));
            _deploymentEvets = deploymentEvets ?? throw new ArgumentNullException(nameof(deploymentEvets));
        }

        public void Enable()
        {
            _deploymentEvets.Finished += OnSetMoveState;
        }

        public void Disable()
        {
            _deploymentEvets.Finished -= OnSetMoveState;
        }

        private void OnSetMoveState()
        {
            StateChanged?.Invoke(_moveState);
        }
    }
}