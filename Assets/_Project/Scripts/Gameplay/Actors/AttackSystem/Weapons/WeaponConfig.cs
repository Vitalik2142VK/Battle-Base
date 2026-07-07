using BattleBase.Gameplay.Actors.AttackSystem.Ammo;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Weapons
{
    [CreateAssetMenu(
    fileName = nameof(WeaponConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(WeaponConfig))]
    public class WeaponConfig : ScriptableObject, IWeaponConfig
    {
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private DamageConfig _damageConfig;

        [Header("Specifications")]
        [SerializeField][Min(5f)] private float _firingRange = 20f;
        [SerializeField][Min(0.1f)] private float _rateShooting = 0.5f;
        [SerializeField][Min(1f)] private float _speedReload = 2f;
        [SerializeField][Min(1)] private int _numberShells = 1;

        public IProjectileConfig ProjectileConfig => _projectileConfig;

        public IDamageConfig DamageConfig => _damageConfig;

        public float Range => _firingRange;

        public float RateShooting => _rateShooting;

        public float SpeedReload => _speedReload;

        public int NumberShells => _numberShells;
    }
}