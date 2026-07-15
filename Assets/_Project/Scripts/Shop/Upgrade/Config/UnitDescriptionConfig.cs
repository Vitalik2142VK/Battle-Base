using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UnitDescriptionConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UnitDescriptionConfig))]
    public class UnitDescriptionConfig : ScriptableObject, ILanguageTextsSet
    {
        [SerializeField] private LanguageTextsSet _textSet;

        public ITextLangParams Ru => _textSet.Ru;

        public ITextLangParams En => _textSet.En;

        public ITextLangParams Tr => _textSet.Tr;
    }
}