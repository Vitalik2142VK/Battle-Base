using System;
using BattleBase.Abstract;
using BattleBase.AudioService;
using BattleBase.SaveService;
using BattleBase.Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.Mediators
{
    public class AudioVolumeSavingMediator : MediatorBase, IInjectable, ISaveable
    {
        [SerializeField] private Slider _generalSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private AudioMixer _mixer;

        private VolumeModifier _generalModifier;
        private VolumeModifier _musicModifier;
        private VolumeModifier _sfxModifier;
        private IAudioVolumeSaver _saver;

        [Inject]
        public void Construct(IAudioVolumeSaver saver) =>
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

        private void OnDestroy()
        {
            _generalModifier?.Dispose();
            _musicModifier?.Dispose();
            _sfxModifier?.Dispose();
        }

        public override void Init()
        {
            _generalModifier = new(_mixer, _generalSlider, Constants.GeneralVolumeGroup);
            _musicModifier = new(_mixer, _musicSlider, Constants.MusicVolumeGroup);
            _sfxModifier = new(_mixer, _sfxSlider, Constants.SfxVolumeGroup);
        }

        public void Load()
        {
            _generalSlider.value = _saver.GeneralVolume;
            _musicSlider.value = _saver.MusicVolume;
            _sfxSlider.value = _saver.SfxVolume;
        }

        public void Save()
        {
            _saver.SetGeneralVolume(_generalSlider.value);
            _saver.SetMusicVolume(_musicSlider.value);
            _saver.SetSfxVolume(_sfxSlider.value);
        }
    }
}