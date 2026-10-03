using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Deactevators
{
    [CreateAssetMenu(
    fileName = nameof(EconomicDeactivatorScore),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(EconomicDeactivatorScore))]
    public class EconomicDeactivatorScore : TacticDeactivatorScore, ITacticDeactivatorScore
    {
        [field: SerializeField] public TeamType TeamType { get; private set; }

        [field: SerializeField] public int MaterialsForDeactivate { get; private set; } = 1000;

        public override Type Type => typeof(EconomicDeactivatorScore);

        private void OnValidate()
        {
            if (MaterialsForDeactivate < 1)
                MaterialsForDeactivate = 1;
        }
    }
}