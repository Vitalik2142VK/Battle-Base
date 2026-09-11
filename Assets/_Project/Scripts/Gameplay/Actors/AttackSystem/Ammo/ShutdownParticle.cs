using BattleBase.Gameplay.Actors.Visual.Particle;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ShutdownParticle : MonoBehaviour
    {
        [SerializeField] private ParticleView _prefab;
        [SerializeField][Min(0.1f)] private float _size;

        private IParticleSpawner _spawner;
        private Transform _transform;
        private string _particleId;

        private void Awake()
        {
            _transform = transform;
            _particleId = _prefab.Id;
        }

        private void OnDisable()
        {
            IParticle particle = _spawner.Spawn(_particleId);
            particle.SetPosition(_transform.position);
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