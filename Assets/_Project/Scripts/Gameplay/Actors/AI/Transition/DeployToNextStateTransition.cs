using BattleBase.Gameplay.Actors.DeploymentSystem;
using System;

namespace BattleBase.Gameplay.Actors.AI.Transition
{
    public class DeployToNextStateTransition : IStateTransition
    {
        private readonly IActorState _deafultState;
        private readonly IDeploymentEvets _deploymentEvets;

        private IActivatedState _activatedState;

        public event Action<IActorState> StateChanged;

        public DeployToNextStateTransition(IActorState deafultState, IDeploymentEvets deploymentEvets)
        {
            _deafultState = deafultState ?? throw new ArgumentNullException(nameof(deafultState));
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

        public void SetActivatedState(IActivatedState activatedState)
        {
            _activatedState = activatedState ?? throw new ArgumentNullException(nameof(activatedState));
        }

        private void OnSetMoveState()
        {
            IActorState actorState;

            if (_activatedState != null && _activatedState.CanAction)
                actorState = _activatedState.ActorState;
            else 
                actorState = _deafultState;

            StateChanged?.Invoke(actorState);
        }
    }
}