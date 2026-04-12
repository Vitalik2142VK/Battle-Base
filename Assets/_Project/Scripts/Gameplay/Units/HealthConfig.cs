using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    [CreateAssetMenu(
        fileName = nameof(HealthConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(UnitConfig) + "/" + nameof(HealthConfig))]
    public class HealthConfig : ScriptableObject, IHealthConfig
    {
        [SerializeField][Min(10f)] private float _maxHealth = 100f;
        [SerializeField][Range(0f, 0.95f)] private float _armorCoefficient = 0.2f;

        public float MaxHealth => _maxHealth;

        public float ArmorCoefficient => _armorCoefficient;
    }
}
