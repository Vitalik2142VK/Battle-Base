using BattleBase.Core;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class ParticleFactory : MonoBehaviour, IFactory<ParticleView>
    {
        [SerializeField] private ParticleView _prefab;

        private IObjectResolver _resolver;

        public string TrailParticleId => _prefab.Id;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new System.ArgumentNullException(nameof(resolver));
        }

        public ParticleView Create() =>
            _resolver.Instantiate(_prefab);
    }
}