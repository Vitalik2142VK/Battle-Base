using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public abstract class ActorSpawnerSource : ActorComponentSource, IActorSpawnerSource
    {
        [SerializeField] private ActorConfig[] _actorsConfigs;

        public IEnumerable<IActorConfig> ActorsConfigs => _actorsConfigs;
    }
}