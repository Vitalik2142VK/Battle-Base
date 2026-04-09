using System.Collections.Generic;
using BattleBase.Abstract;
using BattleBase.Services.Audio;
using BattleBase.UI.PopUps;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class GameBootstrap : BootstrapBase 
    {
        [SerializeField] private List<PopUp> _popUps;
        [SerializeField] private List<MediatorBase> _mediators;

        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService;

        private void Start()
        {
            _audioService.Music.PlayGame();

            foreach (PopUp popUp in _popUps)
            {
                popUp.Init();
                popUp.FastHide();
            }

            foreach (MediatorBase mediator in _mediators)
                mediator.Init();
        }
    }
}