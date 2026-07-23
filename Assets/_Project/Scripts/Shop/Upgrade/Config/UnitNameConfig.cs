using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UnitNameConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UnitNameConfig))]
    public class UnitNameConfig : ScriptableObject
    {
        [SerializeField] private LanguageTextsSet _unitName;
        [SerializeField] private LanguageTextsSet _description;

        public ILanguageTextsSet Name => _unitName;

        public ILanguageTextsSet Description => _description;
    }
}