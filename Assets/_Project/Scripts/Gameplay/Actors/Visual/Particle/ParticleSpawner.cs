using BattleBase.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class ParticleSpawner : MonoBehaviour, IParticleSpawner
    {
        [SerializeField] private List<ParticleFactory> _factories;
        [SerializeField] private Transform _container;

        private IdPoolRegistry<ParticleView, ParticleFactory> _poolRegistry;

        private void OnValidate()
        {
            if (_container == null)
                _container = transform;

            for (int i = 0; i < _factories.Count; i++)
            {
                if (_factories[i] == null)
                    _factories.RemoveAt(i--);
            }
        }

        private void Awake()
        {
            _poolRegistry = new IdPoolRegistry<ParticleView, ParticleFactory>(
                _factories,
                _container,
                factory => factory.TrailParticleId);
        }

        public IParticle Spawn(string particleId) =>
            _poolRegistry.Spawn(particleId);
    }
}