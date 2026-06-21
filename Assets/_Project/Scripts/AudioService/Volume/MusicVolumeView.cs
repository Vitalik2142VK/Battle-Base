using System;
using BattleBase.DI;
using BattleBase.Utils.Constants;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.AudioService
{
    [RequireComponent(typeof(Slider))]
    public class MusicVolumeView : MonoBehaviour, IInjectable
    {
        private Slider _slider;

        private VolumeSliderBinder _binder;

        [Inject]
        public void Construct(AudioMixer mixer, AudioVolumeModel volumeModel)
        {
            _slider = GetComponent<Slider>();

            _binder = new VolumeSliderBinder(
                _slider,
                mixer != null ? mixer : throw new ArgumentNullException(nameof(mixer)),
                volumeModel ?? throw new ArgumentNullException(nameof(volumeModel)),
                () => volumeModel.Music,
                value => volumeModel.SetMusicVolume(value),
                AudioMixerGroupNames.Music
            );
        }

        private void OnEnable() =>
            _binder.Enable();

        private void OnDisable() =>
            _binder.Disable();

        private void OnDestroy() =>
            _binder.Dispose();
    }
}