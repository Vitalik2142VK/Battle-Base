using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    [CreateAssetMenu(
    fileName = nameof(DamageConfig),
    menuName = Constants.ConfigsAssetMenuPath + nameof(UnitConfig) + "/" + nameof(DamageConfig))]
    public class DamageConfig : ScriptableObject, IDamageConfig
    {
        [SerializeField] private DamageMask _damageMask;
        [SerializeField][Min(1f)] private float _damage = 20f;

        public DamageMask DamageMask => _damageMask;

        public float Damage => _damage;
    }
}
