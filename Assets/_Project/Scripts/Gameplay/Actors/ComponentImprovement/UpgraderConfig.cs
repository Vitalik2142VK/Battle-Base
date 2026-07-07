using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    [CreateAssetMenu(
    fileName = nameof(UpgraderConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(UpgraderConfig))]
    public class UpgraderConfig : ScriptableObject, IUpgraderConfig
    {
        [SerializeField][Min(0.01f)] private float _damageCoefficientByLevel = 0.1f;
        [SerializeField][Min(0.01f)] private float _healtheCoefficientByLevel = 0.2f;

        public float DamageCoefficientByLevel => _damageCoefficientByLevel;

        public float HealtheCoefficientByLevel => _healtheCoefficientByLevel;
    }
}