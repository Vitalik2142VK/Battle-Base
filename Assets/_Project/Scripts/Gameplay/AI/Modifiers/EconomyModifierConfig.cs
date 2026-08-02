using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Modifiers
{
    [CreateAssetMenu(fileName = nameof(EconomyModifierConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(EconomyModifierConfig))]
    public class EconomyModifierConfig : ScoreModifierConfig, IEconomyModifierConfig
    {
        [SerializeField][Min(100)] private int _minMaterialsForActivation = 1000;

        public int MinMaterialsForActivation => _minMaterialsForActivation;

        public override ModifierType Type => ModifierType.Economy;
    }
}