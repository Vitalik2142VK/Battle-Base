using BattleBase.Localization;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(ShopUnitItemConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ShopUnitItemConfig))]
    public class ShopUnitItemConfig : ScriptableObject, IShopUnitItemInfo
    {
        [SerializeField] private Sprite _preview;
        [SerializeField] private UnitNameConfig _unitName;
        [SerializeField] private ShopUpgradeStatsConfig _stats;

        public Sprite Preview => _preview;

        public ILanguageTextsSet UnitName => _unitName;

        public IShopUpgradeStatsInfo PanelInfo => _stats;
    }
}