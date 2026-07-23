using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(TerritoryConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(TerritoryConfig))]
    public class TerritoryConfig : ScriptableObject, ITerritoryInfo
    {
        [SerializeField] private LanguageTextsSet _territoryName;
        [SerializeField][Min(0)] private int _creditsForFirstVictory;

        public ILanguageTextsSet TerritoryName => _territoryName;

        public int CreditsForFirstVictory => _creditsForFirstVictory;
    }
}