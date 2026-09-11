namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public interface IDeployment : IActorComponent, IUpdateable, IDeploymentEvets
    {
        public void AddDisablingComponent(IActorComponent component);

        public void Activate();

        public void EnabledComponents();
    }
}
