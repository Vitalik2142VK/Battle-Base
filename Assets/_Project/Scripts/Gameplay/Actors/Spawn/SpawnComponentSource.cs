using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(SpawnComponentSource),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(SpawnComponentSource))]
    public class SpawnComponentSource : ActorComponentSource, ISpawnComponentSource
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IEnumerable<IActorConfig> ActorsConfigs => _actorsConfigs;
    }
}