using BattleBase.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual
{
    public class TrailParticleSpawner : MonoBehaviour, ITrailParticleSpawner
    {
        [SerializeField] private List<TrailParticleFactory> _factories;
        [SerializeField] private Transform _container;

        private IdPoolRegistry<TrailParticle, TrailParticleFactory> _poolRegistry;

        private void OnValidate()
        {
            if (_container == null)
                _container = transform;
        }

        private void Awake()
        {
            _poolRegistry = new IdPoolRegistry<TrailParticle, TrailParticleFactory>(
                _factories,
                _container,
                factory => factory.TrailParticleId);
        }

        public ITrailParticle Spawn(string trailParticleId) =>
            _poolRegistry.Spawn(trailParticleId);
    }
}