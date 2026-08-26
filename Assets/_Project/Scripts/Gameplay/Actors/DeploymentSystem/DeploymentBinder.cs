using System;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class DeploymentBinder : IActorComponentBinder
    {
        public void Bind(IActor actor, IActorView view)
        {
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (actor.TryGetComponent(out IDeployment deployment) == false)
                return;

            if (view.TryGetViewComponent(out IDeploymentView deploymentView))
                deploymentView.Init(deployment);
        }
    }
}
