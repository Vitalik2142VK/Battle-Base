using BattleBase.Services.Audio;
using UnityEngine;
using VContainer;

namespace BattleBase.UI.PopUps
{
    public class PopUpAudio : MonoBehaviour
    {
        [SerializeField] private PopUpAudioConfig _config;

        private IAudioService _audioService;

        [Inject]
        public void Construct(IAudioService audioService) =>
            _audioService = audioService;

        public void PlayShowing() =>
            PlaySound(_config.ShowingClip);

        public void PlayHidding() =>
            PlaySound(_config.HiddingClip);

        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
                _audioService?.Sfx.PlayOneShot(clip);
        }
    }
}