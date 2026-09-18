using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UpgradeStatsConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UpgradeStatsConfig))]
    public class UpgradeStatsConfig : ScriptableObject, IShopUpgradeStatsInfo
    {
        [SerializeField] public UpgradeButtonInfo _damageInfo;
        [SerializeField] public UpgradeButtonInfo _armorInfo;
        [SerializeField] public UpgradeButtonInfo _buildTimeInfo;

        public IUpgradeInfo DamageInfo => _damageInfo;

        public IUpgradeInfo HealthInfo => _armorInfo;

        public IUpgradeInfo BuildTimeInfo => _buildTimeInfo;
    }
}