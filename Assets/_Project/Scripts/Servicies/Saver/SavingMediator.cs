using BattleBase.Abstract;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.Services.SaveService
{
    public class SavingMediator : MediatorBase, IInjectable
    {
        [SerializeField] private Slider _generalSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private ISaver _saver;

        [Inject]
        public void Construct(ISaver saver) =>
            _saver = saver;

        public override void Init()
        {
            _generalSlider.value = _saver.GeneralVolume;
            _musicSlider.value = _saver.MusicVolume;
            _sfxSlider.value = _saver.SfxVolume;
        }

        private void OnDisable()
        {
            _saver.SetGeneralVolume(_generalSlider.value);
            _saver.SetMusicVolume(_musicSlider.value);
            _saver.SetSfxVolume(_sfxSlider.value);
            _saver.Save();
        }
    }
}