using BattleBase.Gameplay.HealthSystem;
using BattleBase.Gameplay.Movement;
using BattleBase.Gameplay.Weapons;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [CreateAssetMenu(
        fileName = nameof(UnitConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(UnitConfig) + "/" + nameof(UnitConfig))]
    public class UnitConfig : ScriptableObject, IUnitConfig
    {
        [SerializeField] private UnitView _prefab;
        [SerializeField] private UnitData _data;
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField][Min(3f)] private float _constructionTime = 5f;

        public UnitView Prefab => _prefab;

        public IUnitData Info => _data;

        public IHealthConfig HealthConfig => _healthConfig;

        public IWeaponConfig WeaponConfig => _weaponConfig;

        public IMovementConfig MovementConfig => _movementConfig;

        public float ConstructionTime => _constructionTime;
    }
}
