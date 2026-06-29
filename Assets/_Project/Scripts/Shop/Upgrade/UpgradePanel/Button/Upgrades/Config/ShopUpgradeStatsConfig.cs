using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(ShopUpgradeStatsConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(ShopUpgradeStatsConfig))]
    public class ShopUpgradeStatsConfig : ScriptableObject, IShopUpgradeStatsInfo
    {
        [SerializeField] public UpgradeButtonInfo _damageInfo;
        [SerializeField] public UpgradeButtonInfo _armorInfo;
        [SerializeField] public UpgradeButtonInfo _buildTimeInfo;

        public IUpgradeButtonInfo DamageInfo => _damageInfo;

        public IUpgradeButtonInfo ArmorInfo => _armorInfo;

        public IUpgradeButtonInfo BuildTimeInfo => _buildTimeInfo;
    }
}