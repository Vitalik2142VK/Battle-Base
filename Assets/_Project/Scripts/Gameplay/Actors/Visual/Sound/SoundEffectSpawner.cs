using BattleBase.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectSpawner : MonoBehaviour, ISoundEffectSpawner
    {
        [SerializeField] private List<SoundEffectFactory> _factories;
        [SerializeField] private Transform _container;

        private IdPoolRegistry<SoundEffect, SoundEffectFactory> _poolRegistry;

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
            _poolRegistry = new IdPoolRegistry<SoundEffect, SoundEffectFactory>(
                _factories,
                _container,
                factory => factory.TrailParticleId);
        }

        public ISoundEffect Spawn(string particleId) =>
            _poolRegistry.Spawn(particleId);
    }
}