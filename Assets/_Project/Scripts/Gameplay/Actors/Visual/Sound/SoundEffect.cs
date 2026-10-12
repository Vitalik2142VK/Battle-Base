using BattleBase.Core;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffect : MonoBehaviour, ISoundEffect, ISoundEffectData, IPoolable<SoundEffect>
    {
        [SerializeField] private AudioSource _audioSource;

        private Transform _transform;

        public event Action<SoundEffect> Deactivated;

        public string Id => gameObject.name;

        public float MaxDistance => _audioSource.maxDistance;

        private void Awake()
        {
            _transform = transform;
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