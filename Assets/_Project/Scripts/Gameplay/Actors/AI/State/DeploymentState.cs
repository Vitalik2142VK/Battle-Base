using BattleBase.Gameplay.Actors.DeploymentSystem;

namespace BattleBase.Gameplay.Actors.AI.State
{
    public class DeploymentState : IActorState
    {
        public readonly IDeployment _deployment;

        public DeploymentState(IDeployment deployment)
        {
            _deployment = deployment ?? throw new System.ArgumentNullException(nameof(deployment));
        }

        public void Enter() => 
            _deployment.Activate();

        public void Exit() => 
            _deployment.EnabledComponents();
    }
}
