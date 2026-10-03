using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Deactevators
{
    [CreateAssetMenu(
    fileName = nameof(EconomicDeactevatorScore),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(EconomicDeactevatorScore))]
    public class EconomicDeactevatorScore : TacticDeactevatorScore, ITacticDeactevatorScore
    {
        [SerializeField][Min(100)] private int _materialsForDeactivate = 1000;

        public int MaterialsForDeactivate => _materialsForDeactivate;
    }
}