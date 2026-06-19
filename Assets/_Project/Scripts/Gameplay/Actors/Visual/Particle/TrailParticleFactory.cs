using BattleBase.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.Visual.Particle
{
    public class TrailParticleFactory : MonoBehaviour, IFactory<TrailParticle>
    {
        [SerializeField] private TrailParticle _trailPrefab;

        private IObjectResolver _resolver;

        public string TrailParticleId => _trailPrefab.Id;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new System.ArgumentNullException(nameof(resolver));
        }

        public TrailParticle Create() =>
            _resolver.Instantiate(_trailPrefab);
    }
}