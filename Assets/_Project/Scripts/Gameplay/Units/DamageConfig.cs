using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    [CreateAssetMenu(
    fileName = nameof(DamageConfig),
    menuName = Constants.ConfigsAssetMenuName + "/" + nameof(UnitConfig) + "/" + nameof(DamageConfig))]
    public class DamageConfig : ScriptableObject, IDamageConfig
    {
        [SerializeField][Min(10f)] private float _damage = 20f;
        [SerializeField][Min(0.1f)] private float _rateShooting = 0.5f;
        [SerializeField][Min(1f)] private float _speedReload = 2f;
        [SerializeField][Min(1)] private int _numberShells = 1;

        public float Damage => _damage;

        public float RateShooting => _rateShooting;

        public float SpeedReload => _speedReload;

        public int NumberShells => _numberShells;
    }
}
