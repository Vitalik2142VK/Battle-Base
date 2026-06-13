using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [System.Serializable]
    public class DamageConfig : IDamageConfig
    {
        [SerializeField] private ActorMask _actorMask;
        [SerializeField] private DamageMask _damageMask;
        [SerializeField][Min(1f)] private float _damage = 20f;

        public ActorMask ActorMask => _actorMask;

        public DamageMask DamageMask => _damageMask;

        public float Damage => _damage;
    }
}
