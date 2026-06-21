namespace BattleBase.SaveService
{
    public interface ISaver : IAudioVolumeSaver, IColorSaver, ITerritorySaver, ICreditsSaver
    {
        public void SaveProgress();

        public void ResetProgress();
    }
}