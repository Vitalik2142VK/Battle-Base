using System;
using System.Collections;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundPlaybackLimiter : MonoBehaviour
    {
        [SerializeField] private SoundEffect _prefab;
        [SerializeField][Min(0.001f)] private float _timeLimit = 0.05f;

        private ISoundEffectSpawner _soundEffectSpawner;
        private WaitForSeconds _wait;

        public string SoundEffectId => _prefab.Id;

        public bool CanPlaySound { get; private set; }

        [Inject]
        public void Contruct(ISoundEffectSpawner soundEffectSpawner)
        {
            _soundEffectSpawner = soundEffectSpawner ?? throw new ArgumentNullException(nameof(soundEffectSpawner));
            _wait = new WaitForSeconds(_timeLimit);

            CanPlaySound = true;
        }

        private void OnEnable()
        {
            if (_soundEffectSpawner != null)
                _soundEffectSpawner.Spawned += OnActiveLimit;
        }

        private void OnDisable()
        {
            if (_soundEffectSpawner != null)
                _soundEffectSpawner.Spawned -= OnActiveLimit;
        }

        private void OnActiveLimit(string soundEffectId) 
        {
            if (string.IsNullOrEmpty(soundEffectId))
                throw new ArgumentException(nameof(soundEffectId));

            if (soundEffectId == SoundEffectId && CanPlaySound)
                StartCoroutine(ActiveLimit());
        }

        private IEnumerator ActiveLimit()
        {
            CanPlaySound = false;

            yield return _wait;

            CanPlaySound = true;
        }
    }
}