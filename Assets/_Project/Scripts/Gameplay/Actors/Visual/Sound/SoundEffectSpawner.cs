using BattleBase.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectSpawner : MonoBehaviour, ISoundEffectSpawner
    {
        [SerializeField] private List<SoundEffectFactory> _factories;
        [SerializeField] private Transform _container;
        [SerializeField][Min(10)] private int _maxNumberSounds = 30;

        private IdPoolRegistry<SoundEffect, SoundEffectFactory> _poolRegistry;

        public event Action<string> Spawned;

        private void OnValidate()
        {
            if (_container == null)
                _container = transform;

            SoundEffectFactory[] factories = transform.GetComponentsInChildren<SoundEffectFactory>();
            
            foreach (var factory in factories)
            {
                if (_factories.Contains(factory) == false)
                    _factories.Add(factory);
            }

            if (_factories.Count == 0)
                return;

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
                factory => factory.SoundEffectId,
                _maxNumberSounds);
        }

        public bool TrySpawn(string particleId, out ISoundEffect soundEffect)
        {
            if (_poolRegistry.TryGive(particleId, out SoundEffect effect))
            {
                soundEffect = effect;

                Spawned?.Invoke(particleId);

                return true;
            }

            soundEffect = null;

            return false;
        }
    }
}