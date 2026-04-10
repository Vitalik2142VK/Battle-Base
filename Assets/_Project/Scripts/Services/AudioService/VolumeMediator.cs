using BattleBase.Abstract;
using BattleBase.Static;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BattleBase.Services.Audio
{
    public class VolumeMediator : MediatorBase
    {
        [SerializeField] private AudioMixer _mixer;

        [SerializeField] private Slider _generalSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private VolumeModifier _generalModifier;
        private VolumeModifier _musicModifier;
        private VolumeModifier _sfxModifier;

        public override void Init()
        {
            _generalModifier = new(_mixer, _generalSlider, Constants.GeneralVolumeGroup);
            _musicModifier = new(_mixer, _musicSlider, Constants.MusicVolumeGroup);
            _sfxModifier = new(_mixer, _sfxSlider, Constants.SfxVolumeGroup);
        }

        private void OnDestroy()
        {
            _generalModifier?.Dispose();
            _musicModifier?.Dispose();
            _sfxModifier?.Dispose();
        }
    }
}