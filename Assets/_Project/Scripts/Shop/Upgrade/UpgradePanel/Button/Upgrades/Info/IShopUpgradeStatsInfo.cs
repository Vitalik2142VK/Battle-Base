namespace BattleBase.ShopSystem
{
    public interface IShopUpgradeStatsInfo
    {
        public IUpgradeButtonInfo DamageInfo { get; }

        public IUpgradeButtonInfo ArmorInfo { get; }

        public IUpgradeButtonInfo BuildTimeInfo { get; }
    }
}