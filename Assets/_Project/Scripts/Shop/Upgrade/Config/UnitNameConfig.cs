using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UnitNameConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UnitNameConfig))]
    public class UnitNameConfig : ScriptableObject, ILanguageTextsSet
    {
        [SerializeField] private LanguageTextsSet _unitName;

        public ITextLangParams Ru => _unitName.Ru;

        public ITextLangParams En => _unitName.En;

        public ITextLangParams Tr => _unitName.Tr;
    }
}