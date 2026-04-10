using System;
using UnityEngine;

namespace BattleBase.Services.Audio
{
    public class Sfx : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;

        public void PlayOneShot(AudioClip clip)
        {
            if (clip == null)
                throw new ArgumentNullException(nameof(clip));

            _source.PlayOneShot(clip);
        }
    }
}