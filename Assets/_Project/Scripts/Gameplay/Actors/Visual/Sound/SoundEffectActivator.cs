using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectActivator : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private ISoundEffectSpawner _spawner;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        public void Activate(string particleId)
        {
            ISoundEffect soundEffect = _spawner.Spawn(particleId);
            soundEffect.SetPosition(_spawnPoint.position);
            soundEffect.Play();
        }

        [Inject]
        public void Construct(ISoundEffectSpawner spawner)
        {
            _spawner = spawner ?? throw new System.ArgumentNullException(nameof(spawner));
        }
    }
}