using UnityEngine;

namespace BattleBase.Services.Audio
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public void Play(AudioClip clip)
        {
            _source.clip = clip;
            _source.Play();
        }

        public void Stop()
        {
            if (_source != null)
                _source.Stop();
        }
    }
}