using BattleBase.Core;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour, ISoundEffect, IPoolable<SoundEffect>
    {
        private Transform _transform;
        private AudioSource _audioSource;

        public event Action<SoundEffect> Deactivated;

        public string Id => gameObject.name;

        private void Awake()
        {
            _transform = transform;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_audioSource.isPlaying == false)
                Deactivated?.Invoke(this);
        }

        public void SetPosition(Vector3 position) =>
            _transform.position = position;

        public void Play() =>
            _audioSource.Play();
    }
}