namespace BattleBase.SaveService
{
    public interface IAudioVolumeSaver : ISaver
    {
        public float GeneralVolume { get; }

        public float MusicVolume { get; }

        public float SfxVolume { get; }

        public void SetGeneralVolume(float volume);

        public void SetMusicVolume(float volume);

        public void SetSfxVolume(float volume);
    }
}