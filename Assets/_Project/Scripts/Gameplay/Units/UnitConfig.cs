using BattleBase.Gameplay.Movement;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    [CreateAssetMenu(
        fileName = nameof(UnitConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(UnitConfig) + "/" + nameof(UnitConfig))]
    public class UnitConfig : ScriptableObject, IUnitConfig
    {
        [SerializeField] private UnitView _prefab;
        [SerializeField] private UnitData _data;
        [SerializeField] private HealthConfig _healthConfig;
        [SerializeField] private DamageConfig _damageConfig;
        [SerializeField] private MovementConfig _movementConfig;

        public UnitView Prefab => _prefab;

        public IUnitData Info => _data;

        public IHealthConfig HealthConfig => _healthConfig;

        public IDamageConfig DamageConfig => _damageConfig;

        public IMovementConfig MovementConfig => _movementConfig;

        private void OnValidate()
        {
            if (_prefab == null)
                throw new System.NullReferenceException(nameof(_prefab));

            if (_data == null)
                throw new System.NullReferenceException(nameof(_data));

            if (_healthConfig == null)
                throw new System.NullReferenceException(nameof(_healthConfig));

            if (_damageConfig == null)
                throw new System.NullReferenceException(nameof(_damageConfig));

            if (_movementConfig == null)
                throw new System.NullReferenceException(nameof(_movementConfig));
        }
    }
}
