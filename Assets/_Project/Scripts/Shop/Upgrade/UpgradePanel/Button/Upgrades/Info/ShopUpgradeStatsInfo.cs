using System;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class ShopUpgradeStatsInfo : IUpgradeStatsInfo
    {
        [SerializeField] public UpgradeButtonInfo _damageInfo;
        [SerializeField] public UpgradeButtonInfo _armorInfo;
        [SerializeField] public UpgradeButtonInfo _speedInfo;

        public ShopUpgradeStatsInfo(IUpgradeStatsInfo other)
        {
            _damageInfo = new(other.DamageInfo);
            _armorInfo = new(other.HealthInfo);
            _speedInfo = new(other.SpeedInfo);
        }

        public IUpgradeInfo DamageInfo => _damageInfo;

        public IUpgradeInfo HealthInfo => _armorInfo;

        public IUpgradeInfo SpeedInfo => _speedInfo;

        public void IncreaseDamageLevel() =>
            _damageInfo.Increase();

        public void IncreaseArmorLevel() =>
            _armorInfo.Increase();

        public void IncreaseBuildTimeLevel() =>
            _speedInfo.Increase();

        public void SetDamageLevel(int level) =>
            _damageInfo.SetLevel(level);

        public void SetArmorLevel(int level) =>
            _armorInfo.SetLevel(level);

        public void SetSpeedLevel(int level) =>
            _speedInfo.SetLevel(level);
    }
}