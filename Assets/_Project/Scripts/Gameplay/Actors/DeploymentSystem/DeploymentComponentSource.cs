using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    [CreateAssetMenu(
        fileName = nameof(DeploymentComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(DeploymentComponentSource))]
    public class DeploymentComponentSource : ActorComponentSource, IDeploymentComponentSource
    {
        [SerializeField][Range(1f, 5f)] private float _deployTime = 2f;

        public float DeployTime => _deployTime;
    }
}
