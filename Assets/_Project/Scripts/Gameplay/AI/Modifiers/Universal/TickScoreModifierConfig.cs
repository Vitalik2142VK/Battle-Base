using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers.Univarsal
{
    [CreateAssetMenu(
    fileName = nameof(TickScoreModifierConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(TickScoreModifierConfig))]
    public class TickScoreModifierConfig : ScoreModifierConfig, ITickScoreModifierConfig
    {
        [SerializeField][Range(1, 10)] private int _tickNumber = 5;

        public override ModifierType Type => ModifierType.Tick;

        public int TickNumber => _tickNumber;
    }
}