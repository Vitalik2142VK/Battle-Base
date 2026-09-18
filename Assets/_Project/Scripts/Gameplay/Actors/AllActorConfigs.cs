using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    //todo remove if not used
    public class AllActorConfigs : MonoBehaviour
    {
        [SerializeField] private ActorConfig[] _allActorConfigs;

        private Dictionary<string, ActorConfig> _actorConfigs;

        public IActorConfig GetConfig(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new System.ArgumentException(nameof(id));

            if (_actorConfigs == null)
            {
                _actorConfigs = new Dictionary<string, ActorConfig>();

                foreach (var actorConfig in _allActorConfigs)
                    _actorConfigs.Add(actorConfig.Data.Id, actorConfig);
            }

            return _actorConfigs[id];
        }
    }
}