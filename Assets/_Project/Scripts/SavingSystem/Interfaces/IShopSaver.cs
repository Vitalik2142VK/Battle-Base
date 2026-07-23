namespace BattleBase.SaveService
{
    public interface IShopSaver
    {
        public IShopData ShopData { get; }

        public void SetShopData(IShopData data);
    }
}