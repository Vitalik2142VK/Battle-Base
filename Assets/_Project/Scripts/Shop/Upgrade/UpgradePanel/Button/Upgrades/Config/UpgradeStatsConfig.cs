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
        [Header("Main")]
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
            float growthRate = 1.1f; //default value - 1.1f
            float decay = 0.3f; //default value - 0.3f

            FillPrices(_damageInfo, growthRate, decay);
            FillPrices(_healthInfo, growthRate, decay);

            float speedGrowthRate = 1.2f; //default value - 1.2f
            float speedDecay = 0.4f; //default value - 0.4f

            FillPrices(_speedInfo, speedGrowthRate, speedDecay);
        }

        private static void FillPrices(UpgradeButtonInfo upgradeButtonInfo, float growthRate, float decay)
        {
            IReadOnlyList<int> prices = upgradeButtonInfo.Levels;

            if (prices.Count <= 0)
                return;

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