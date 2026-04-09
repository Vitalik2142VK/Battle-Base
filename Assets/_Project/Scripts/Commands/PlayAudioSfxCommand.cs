using System;
using BattleBase.Abstract;
using BattleBase.Services.Audio;
using UnityEngine;
using VContainer;

namespace BattleBase.Commands
{
    public class PlayAudioSfxCommand : CommandBase, IInjectable
    {
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService ?? throw new ArgumentNullException(nameof(audioService));

        public override void Execute() =>
            _audioService.Sfx.PlayOneShot(_audioClip);
    }
}