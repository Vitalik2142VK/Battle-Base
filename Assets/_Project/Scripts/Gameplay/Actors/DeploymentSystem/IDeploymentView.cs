namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public interface IDeploymentView : IActorViewComponent
    {
        public void Init(IDeploymentEvets deploymentEvets);
    }
}
