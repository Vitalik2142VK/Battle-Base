using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(UpgradeStatsConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(UpgradeStatsConfig))]
    public class UpgradeStatsConfig : ScriptableObject, IUpgradeStatsInfo
    {
        [SerializeField] private UpgradeButtonInfo _damageInfo;
        [SerializeField] private UpgradeButtonInfo _healthInfo;
        [SerializeField] private UpgradeButtonInfo _speedInfo;

        public IUpgradeInfo DamageInfo => _damageInfo;

        public IUpgradeInfo HealthInfo => _healthInfo;

        public IUpgradeInfo SpeedInfo => _speedInfo;
#if UNITY_EDITOR
        [ContextMenu(nameof(UpgradePrices))]
        private void UpgradePrices()
        {
            FillPrices(_damageInfo);
            FillPrices(_healthInfo);
            FillPrices(_speedInfo);
        }

        private static void FillPrices(UpgradeButtonInfo upgradeButtonInfo)
        {
            IReadOnlyList<int> prices = upgradeButtonInfo.Levels;

            if (prices.Count <= 0)
                return;

            float growthRate = upgradeButtonInfo.GrowthRate;
            float decay = upgradeButtonInfo.Decay;
            float baseValue = prices[0];

            for (int i = 1; i < prices.Count; i++)
            {
                int price = CalculateSoftExponential(baseValue, growthRate, decay, i);
                upgradeButtonInfo.SetPrice(i, price);
            }
        }

        private static int CalculateSoftExponential(float baseValue, float growthRate, float decay, int level) =>
            (int)(baseValue * Mathf.Pow(1 + growthRate, decay * level));
#endif
    }
}