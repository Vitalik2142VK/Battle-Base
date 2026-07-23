using System;
using BattleBase.SaveService;
using UnityEngine;

namespace BattleBase.AudioService
{
    public class AudioVolumeModel : ISaveable
    {
        private readonly IAudioVolumeSaver _saver;

        public AudioVolumeModel(IAudioVolumeSaver saver)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

            Load();
        }

        public event Action Changed;

        public float General { get; private set; }

        public float Music { get; private set; }

        public float Sfx { get; private set; }

        public void SetGeneralVolume(float value)
        {
            General = Mathf.Clamp01(value);

            Changed?.Invoke();
        }

        public void SetMusicVolume(float value)
        {
            Music = Mathf.Clamp01(value);

            Changed?.Invoke();
        }

        public void SetSfxVolume(float value)
        {
            Sfx = Mathf.Clamp01(value);

            Changed?.Invoke();
        }

        public void Load()
        {
            IVolumeData data = _saver.VolumeData;
            General = Mathf.Clamp01(data.GeneralVolume);
            Music = Mathf.Clamp01(data.MusicVolume);
            Sfx = Mathf.Clamp01(data.SfxVolume);
        }

        public void Save()
        {
            VolumeData data = new(General, Music, Sfx);
            _saver.SetVolumeData(data);
        }
    }
}