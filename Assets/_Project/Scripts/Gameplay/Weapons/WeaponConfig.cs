using BattleBase.Gameplay.Units;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Weapons
{
    [CreateAssetMenu(
        fileName = nameof(WeaponConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(UnitConfig) + "/" + nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject, IWeaponConfig
    {
        [SerializeField] private DamageConfig _damageConfig;
        [SerializeField][Min(0.1f)] private float _rateShooting = 0.5f;
        [SerializeField][Min(1f)] private float _speedReload = 2f;
        [SerializeField][Min(1)] private int _numberShells = 1;

        public IDamageConfig DamageConfig => _damageConfig;

        public float RateShooting => _rateShooting;

        public float SpeedReload => _speedReload;

        public int NumberShells => _numberShells;
    }
}