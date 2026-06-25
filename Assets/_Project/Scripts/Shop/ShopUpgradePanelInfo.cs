using System;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUpgradePanelInfo
    {
        [field: SerializeField] public UpgradeButtonInfo DamageInfo;
        [field: SerializeField] public UpgradeButtonInfo ArmorInfo;
        [field: SerializeField] public UpgradeButtonInfo BuildTimeInfo;
    }
}