using System.Collections.Generic;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    [CreateAssetMenu(
        fileName = nameof(MultiSpawnComponentSource),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(MultiSpawnComponentSource))]
    public class SingleSpawnComponentSource : ActorComponentSource, ISpawnComponentSource
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IEnumerable<IActorConfig> ActorsConfigs => _actorsConfigs;
    }
}