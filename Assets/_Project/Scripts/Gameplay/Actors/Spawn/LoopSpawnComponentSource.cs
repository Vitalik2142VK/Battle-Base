using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(LoopSpawnComponentSource),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(LoopSpawnComponentSource))]
    public class LoopSpawnComponentSource : ActorComponentSource, ISpawnComponentSource
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IEnumerable<IActorConfig> ActorsConfigs => _actorsConfigs;
    }
}