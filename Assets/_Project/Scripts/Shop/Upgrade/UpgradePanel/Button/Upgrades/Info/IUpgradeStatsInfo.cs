namespace BattleBase.ShopSystem
{
    public interface IUpgradeStatsInfo
    {
        public IUpgradeInfo DamageInfo { get; }

        public IUpgradeInfo HealthInfo { get; }

        public IUpgradeInfo SpeedInfo { get; }
    }
}