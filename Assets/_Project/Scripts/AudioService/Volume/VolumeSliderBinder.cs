using System;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.AudioService
{
    public sealed class VolumeSliderBinder : IDisposable
    {
        private readonly Slider _slider;
        private readonly AudioVolumeModel _volumeModel;
        private readonly Func<float> _getVolume;
        private readonly Action<float> _setVolume;

        private bool _enabled;

        public VolumeSliderBinder(
            Slider slider,
            AudioVolumeModel volumeModel,
            Func<float> getVolume,
            Action<float> setVolume)
        {
            _slider = slider != null ? slider : throw new ArgumentNullException(nameof(slider));
            _volumeModel = volumeModel ?? throw new ArgumentNullException(nameof(volumeModel));
            _getVolume = getVolume ?? throw new ArgumentNullException(nameof(getVolume));
            _setVolume = setVolume ?? throw new ArgumentNullException(nameof(setVolume));

            Enable();
        }

        public void Dispose() =>
            Disable();

        public void Enable()
        {
            if(_enabled)
                return;

            _enabled = true;
            _slider.onValueChanged.AddListener(OnSliderChanged);
            _volumeModel.Changed += OnModelChanged;
            OnModelChanged();
        }

        public void Disable()
        {
            if(_enabled == false)
                return;

            _enabled = false;
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
            _volumeModel.Changed -= OnModelChanged;
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
        }
    }
}