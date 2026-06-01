using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(MultiSpawnComponentSource),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(MultiSpawnComponentSource))]
    public class MultiSpawnComponentSource : ActorComponentSource, ISpawnComponentSource
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IEnumerable<IActorConfig> ActorsConfigs => _actorsConfigs;
    }
}