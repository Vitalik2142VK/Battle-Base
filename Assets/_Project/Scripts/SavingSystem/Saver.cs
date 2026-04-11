using System;
using UnityEngine;

namespace BattleBase.SaveService
{
    public class Saver : ISaver
    {
        private readonly ISaveSystem _saveSystem;

        public Saver(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem ?? throw new ArgumentNullException(nameof(saveSystem));
        }

        public float GeneralVolume => Data.GeneralVolume;

        public float MusicVolume => Data.MusicVolume;

        public float SfxVolume => Data.SfxVolume;

        private SavesData Data => _saveSystem.Data;

        public void Save() =>
            _saveSystem.SaveProgress();

        public void SetGeneralVolume(float volume) =>
            Data.GeneralVolume = Mathf.Clamp01(volume);

        public void SetMusicVolume(float volume) =>
            Data.MusicVolume = Mathf.Clamp01(volume);

        public void SetSfxVolume(float volume) =>
            Data.SfxVolume = Mathf.Clamp01(volume);
    }
}