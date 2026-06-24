using UnityEngine;

namespace BattleBase.Shop
{
    public readonly struct ShopUpgradePanelInfo
    {
        public readonly UpgradeButtonInfo DamageInfo;
        public readonly UpgradeButtonInfo ArmorInfo;
        public readonly UpgradeButtonInfo BuildTimeInfo;
        public readonly Sprite Preview;

        public ShopUpgradePanelInfo(
            UpgradeButtonInfo damageInfo, 
            UpgradeButtonInfo armorInfo, 
            UpgradeButtonInfo buildTimeInfo, 
            Sprite preview)
        {
            DamageInfo = damageInfo;
            ArmorInfo = armorInfo;
            BuildTimeInfo = buildTimeInfo;
            Preview = preview;
        }
    }
}