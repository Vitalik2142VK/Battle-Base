using BattleBase.Core;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffect : MonoBehaviour, ISoundEffect, ISoundEffectData, IPoolable<SoundEffect>
    {
        [SerializeField] private AudioSource _audioSource;

        private Transform _transform;
        private float _defaultPitch;

        public event Action<SoundEffect> Deactivated;

        public string Id => gameObject.name;

        public float MaxDistance => _audioSource.maxDistance;

        private void Awake()
        {
            _transform = transform;
            _defaultPitch = _audioSource.pitch;
        }

        private void Update()
        {
            if (_audioSource.isPlaying == false)
            {
                if (_audioSource.pitch != _defaultPitch)
                    _audioSource.pitch = _defaultPitch;

                Deactivated?.Invoke(this);
            }
        }

        public void SetPosition(Vector3 position) =>
            _transform.position = position;

        public void Play(float rangePitch = 0)
        {
            if (rangePitch > 0)
            {
                float maxPitch = _defaultPitch + rangePitch;
                float minPitch = _defaultPitch - rangePitch;
                float randomPitch = UnityEngine.Random.Range(minPitch, maxPitch);
                _audioSource.pitch = randomPitch;
            }

            _audioSource.Play();
        }
    }
}