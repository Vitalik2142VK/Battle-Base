using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    [System.Serializable]
    public class DamageConfig : IDamageConfig
    {
        [SerializeField] private TargetingProfile _targetingProfile;
        [SerializeField] private DamageMask _damageMask;
        [SerializeField][Min(1f)] private float _damage = 20f;

        public ITargetingProfile TargetingProfile => _targetingProfile;

        public DamageMask DamageMask => _damageMask;

        public float Damage => _damage;
    }
}
