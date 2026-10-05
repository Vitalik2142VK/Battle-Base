using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class ParticleActivator : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private IParticleSpawner _spawner;

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
        }

        public void Activate(string particleId, float size = 1f)
        {
            IParticle particle = _spawner.Spawn(particleId);
            particle.SetPosition(_spawnPoint.position);
            particle.SetSize(size);
            particle.Play();
        }

        [Inject]
        public void Construct(IParticleSpawner spawner)
        {
            _spawner = spawner ?? throw new System.ArgumentNullException(nameof(spawner));
        }
    }
}