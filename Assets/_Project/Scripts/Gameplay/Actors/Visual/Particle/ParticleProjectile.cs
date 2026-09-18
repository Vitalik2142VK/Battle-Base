using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class ParticleProjectile : MonoBehaviour
    {
        [SerializeField] private ParticleView _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _size = 1f;

        private IParticleSpawner _spawner;
        private string _particleId;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        private void Awake()
        {
            _particleId = _prefab.Id;
        }

        public void Play()
        {
            IParticle particle = _spawner.Spawn(_particleId);
            particle.SetPosition(_spawnPoint.position);
            particle.SetSize(_size);
            particle.Play();
        }

        [Inject]
        public void Construct(IParticleSpawner spawner)
        {
            _spawner = spawner ?? throw new System.ArgumentNullException(nameof(spawner));
        }
    }
}