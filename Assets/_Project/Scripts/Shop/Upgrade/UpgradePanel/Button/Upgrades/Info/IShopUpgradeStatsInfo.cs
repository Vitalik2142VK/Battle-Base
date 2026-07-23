namespace BattleBase.ShopSystem
{
    public interface IShopUpgradeStatsInfo
    {
        public IUpgradeInfo DamageInfo { get; }

        public IUpgradeInfo HealthInfo { get; }

        public IUpgradeInfo BuildTimeInfo { get; }
    }
}