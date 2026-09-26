namespace BattleBase.SaveService
{
    public interface ITerritorySaver
    {
        public ITerritoriesData TerritoryData { get; }

        public void SetTerritoryData(ITerritoriesData data);
    }
}