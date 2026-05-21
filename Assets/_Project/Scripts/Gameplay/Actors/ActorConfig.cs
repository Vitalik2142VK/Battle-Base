using BattleBase.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
        fileName = nameof(ActorConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(ActorConfig) + "/" + nameof(ActorConfig))]
    public class ActorConfig : ScriptableObject, IActorConfig
    {
        [SerializeField] private ActorData _data;
        [SerializeField] private List<ActorComponentSource> _components;

        public IActorData Data => _data;

        public IEnumerable<IComponentSource> GetComponentSources()
        {
            return _components
                .Where(c => c is IComponentSource)
                .Select(c => (IComponentSource)c)
                .ToList();
        }
    }
}
