using System;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUpgradeStatsInfo : IShopUpgradeStatsInfo
    {
        [SerializeField] public UpgradeButtonInfo _damageInfo;
        [SerializeField] public UpgradeButtonInfo _armorInfo;
        [SerializeField] public UpgradeButtonInfo _buildTimeInfo;

        public ShopUpgradeStatsInfo(IShopUpgradeStatsInfo other)
        {
            _damageInfo = new(other.DamageInfo);
            _armorInfo = new(other.ArmorInfo);
            _buildTimeInfo = new(other.BuildTimeInfo);
        }

        public IUpgradeButtonInfo DamageInfo => _damageInfo;

        public IUpgradeButtonInfo ArmorInfo => _armorInfo;

        public IUpgradeButtonInfo BuildTimeInfo => _buildTimeInfo;

        public void IncreaseDamageLevel() =>
            _damageInfo.Increase();

        public void IncreaseArmorLevel() =>
            _armorInfo.Increase();

        public void IncreaseBuildTimeLevel() =>
            _buildTimeInfo.Increase();

        public void SetDamageLevel(int level) =>
            _damageInfo.SetLevel(level);

        public void SetArmorLevel(int level) =>
            _armorInfo.SetLevel(level);

        public void SetBuildTimeLevel(int level) =>
            _buildTimeInfo.SetLevel(level);
    }
}