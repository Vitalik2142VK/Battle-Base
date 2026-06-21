using System;
using BattleBase.Utils.Extensions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BattleBase.AudioService
{
    public sealed class VolumeSliderBinder : IDisposable
    {
        private readonly Slider _slider;
        private readonly AudioMixer _mixer;
        private readonly AudioVolumeModel _volumeModel;
        private readonly Func<float> _getVolume;
        private readonly Action<float> _setVolume;
        private readonly string _mixerGroupName;

        public VolumeSliderBinder(
            Slider slider,
            AudioMixer mixer,
            AudioVolumeModel volumeModel,
            Func<float> getVolume,
            Action<float> setVolume,
            string mixerGroupName)
        {
            _slider = slider != null ? slider : throw new ArgumentNullException(nameof(slider));
            _mixer = mixer != null ? mixer : throw new ArgumentNullException(nameof(mixer));
            _volumeModel = volumeModel ?? throw new ArgumentNullException(nameof(volumeModel));
            _getVolume = getVolume ?? throw new ArgumentNullException(nameof(getVolume));
            _setVolume = setVolume ?? throw new ArgumentNullException(nameof(setVolume));
            _mixerGroupName = mixerGroupName ?? throw new ArgumentNullException(nameof(mixerGroupName));
        }

        public void Enable()
        {
            _slider.onValueChanged.AddListener(OnSliderChanged);
            _volumeModel.Changed += OnModelChanged;
            OnModelChanged();
        }

        public void Disable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
            _volumeModel.Changed -= OnModelChanged;
        }

        public void Dispose()
        {
            Disable();
        }

        private void OnSliderChanged(float value)
        {
            float currentVolume = _getVolume();

            if (Mathf.Approximately(currentVolume, value) == false)
                _setVolume(value);
        }

        private void OnModelChanged()
        {
            float currentVolume = _getVolume();

            if (Mathf.Approximately(_slider.value, currentVolume) == false)
                _slider.value = currentVolume;

            float normalized = _slider.value.Remap(_slider.minValue, _slider.maxValue);
            AudioVolumeSetter.SetNormalizedVolume(_mixer, _mixerGroupName, normalized);
        }
    }
}