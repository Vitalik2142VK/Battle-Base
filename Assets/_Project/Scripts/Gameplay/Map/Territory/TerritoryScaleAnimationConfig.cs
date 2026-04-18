using BattleBase.Utils;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(TerritoryScaleAnimationConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TerritoryScaleAnimationConfig))]
    public class TerritoryScaleAnimationConfig : ScriptableObject
    {
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _delay = 0f;
        [SerializeField] private Vector3 _targetScale = Vector3.one * 1.2f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        public float Duration => _duration;

        public float Delay => _delay;

        public Vector3 TargetScale => _targetScale;

        public Ease Ease => _ease;
    }
}