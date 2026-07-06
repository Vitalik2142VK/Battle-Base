using System;
using BattleBase.DI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.AudioService
{
    [RequireComponent(typeof(Slider))]
    public class SfxVolumeView : MonoBehaviour, IInjectable
    {
        private Slider _slider;

        private VolumeSliderBinder _binder;

        [Inject]
        public void Construct(AudioVolumeModel volumeModel)
        {
            _slider = GetComponent<Slider>();

            _binder = new VolumeSliderBinder(
                _slider,
                volumeModel ?? throw new ArgumentNullException(nameof(volumeModel)),
                () => volumeModel.Sfx,
                value => volumeModel.SetSfxVolume(value));
        }

        private void OnEnable() =>
            _binder.Enable();

        private void OnDisable() =>
            _binder.Disable();

        private void OnDestroy() =>
            _binder.Dispose();
    }
}