
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers.Defense
{
    [CreateAssetMenu(
    fileName = nameof(DefenseModifierConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(DefenseModifierConfig))]
    public class DefenseModifierConfig : ScoreModifierConfig, IDefenseModifierConfig
    {
        [SerializeField][Range(0.1f, 1f)] private float _scoreCoefficientForActor = 0.2f;
        [SerializeField][Range(0.1f, 5f)] private float _maxCoefficient = 3f;
        [SerializeField][Range(1, 5)] private int _minActorsForAction = 3; 

        public override ModifierType Type => ModifierType.Defense;

        public float ScoreCoefficientForActor => _scoreCoefficientForActor;

        public float MaxCoefficient => _maxCoefficient;

        public int MinActorsForAction => _minActorsForAction;
    }
}