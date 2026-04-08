using BattleBase.Services.Audio;
using UnityEngine;
using VContainer;

namespace BattleBase.Bootstraps
{
    public class MenuBootstrap : MonoBehaviour 
    { 
        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        private void Start()
        {
            _audioService.Music.PlayMenu();
        }
    }
}