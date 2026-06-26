namespace BattleBase.ShopSystem
{
    public interface IShopUpgradePanelInfo
    {
        public IUpgradeButtonInfo DamageInfo { get; }

        public IUpgradeButtonInfo ArmorInfo { get; }

        public IUpgradeButtonInfo BuildTimeInfo { get; }
    }
}