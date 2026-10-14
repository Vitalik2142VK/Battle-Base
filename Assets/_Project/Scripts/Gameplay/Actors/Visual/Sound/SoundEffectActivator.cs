using BattleBase.Gameplay.Audio;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectActivator : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private ISoundEffectSpawner _spawner;
        private IAudioListner _audioListner;
        private ISoundLimiterController _limiterController;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        public void Activate(ISoundEffectData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (_limiterController.CanPlaySound(data.Id) == false)
                return;

            if (_audioListner.CanListen(_spawnPoint.position, data.MaxDistance) == false)
                return;

            if (_spawner.TrySpawn(data.Id, out ISoundEffect soundEffect) == false)
                return;

            soundEffect.SetPosition(_spawnPoint.position);
            soundEffect.Play();
        }

        [Inject]
        public void Construct(
            ISoundEffectSpawner spawner, 
            IAudioListner audioListner,
            ISoundLimiterController limiterController)
        {
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _audioListner = audioListner ?? throw new ArgumentNullException(nameof(audioListner));
            _limiterController = limiterController ?? throw new ArgumentNullException(nameof(limiterController));
        }
    }
}