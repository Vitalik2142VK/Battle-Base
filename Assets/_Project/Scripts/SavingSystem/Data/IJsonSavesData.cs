namespace BattleBase.SaveService
{
    public interface IJsonSavesData
    {
        public IVolumeData VolumeData { get; }

        public IColorData ColorData { get; }

        public ITerritoriesData TerritoryData { get; }

        public IShopData ShopData { get; }
    }
}