using System;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class DeploymentFactory : IComponentFactory
    {
        public Type SourceType => typeof(DeploymentComponentSource);

        public IActorComponent Create(IComponentSource source)
        {
            if (source is IDeploymentComponentSource deploymentSource == false)
                throw new ArgumentException(
                    $"{nameof(source)} 'source' does not implement {nameof(IDeploymentComponentSource)}");

            return new Deployment(deploymentSource);
        }
    }
}
