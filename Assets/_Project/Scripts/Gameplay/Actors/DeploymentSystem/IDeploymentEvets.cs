using System;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public interface IDeploymentEvets
    {
        public event Action Started;
        public event Action Finished;
    }
}
