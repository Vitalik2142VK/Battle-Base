using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers.Energy
{
    [CreateAssetMenu(fileName = nameof(PowerModifierConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(PowerModifierConfig))]
    public class PowerModifierConfig : ScoreModifierConfig, IPowerModifierConfig
    {
        [SerializeField][Range(1, 20)] private int _maxRemainingEnergy = 10;

        public override ModifierType Type => ModifierType.Power;

        public int MaxRemainingEnergy => _maxRemainingEnergy;
    }
}