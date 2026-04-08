using UnityEngine;
using YG;

namespace BattleBase.Services.SaveService
{
    public class Saver : ISaver
    {
        public float GeneralVolume => Data.GeneralVolume;

        public float MusicVolume => Data.MusicVolume;

        public float SfxVolume => Data.SfxVolume;

        private SavesData Data => YG2.saves.SavesData;

        public void Save() =>
            YG2.SaveProgress();

        public void SetGeneralVolume(float volume) =>
            Data.GeneralVolume = Mathf.Clamp01(volume);

        public void SetMusicVolume(float volume) =>
            Data.MusicVolume = Mathf.Clamp01(volume);

        public void SetSfxVolume(float volume) =>
            Data.SfxVolume = Mathf.Clamp01(volume);
    }
}