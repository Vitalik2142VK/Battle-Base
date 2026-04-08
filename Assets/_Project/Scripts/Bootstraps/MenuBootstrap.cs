using System.Collections.Generic;
using BattleBase.Services.Audio;
using BattleBase.Services.SaveService;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class MenuBootstrap : MonoBehaviour 
    {
        [SerializeField] private List<PopUp> _popUps;
        [SerializeField] private VolumeMediator _volumeMediator;
        [SerializeField] private SavingMediator _savingMediator;

        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void Start()
        {
            _audioService.Music.PlayMenu();

            foreach (PopUp popUp in _popUps)
                popUp.Init();

            _volumeMediator.Init();
            _savingMediator.Init();
        }
    }
}