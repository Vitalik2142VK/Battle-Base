using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    [System.Serializable]
    public class HealthConfig : IHealthConfig
    {
        [SerializeField][Min(10f)] private float _maxHealth = 100f;
        [SerializeField][Range(0f, 0.95f)] private float _armorCoefficient = 0.2f;

        public float MaxHealth => _maxHealth;

        public float ArmorCoefficient => _armorCoefficient;
    }
}
