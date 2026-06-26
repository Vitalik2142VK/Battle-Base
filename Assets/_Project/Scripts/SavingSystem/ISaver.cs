namespace BattleBase.SaveService
{
    public interface ISaver : IAudioVolumeSaver, IColorSaver, ITerritorySaver, IShopSaver
    {
        public void SaveProgress();

        public void ResetProgress();
    }
}