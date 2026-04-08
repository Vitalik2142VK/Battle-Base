using UnityEngine;

namespace BattleBase.Services.Audio
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _menuClip;
        [SerializeField] private AudioClip _gameClip;

        public void Stop()
        {
            if (_source != null)
                _source.Stop();
        }

        public void PlayMenu() =>
            Play(_menuClip);

        public void PlayGame() =>
            Play(_gameClip);

        private void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }
    }
}