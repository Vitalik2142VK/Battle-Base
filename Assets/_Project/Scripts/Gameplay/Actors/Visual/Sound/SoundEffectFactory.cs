using BattleBase.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.Visual.Sound
{
    public class SoundEffectFactory : MonoBehaviour, IFactory<SoundEffect>
    {
        [SerializeField] private SoundEffect _prefab;

        private IObjectResolver _resolver;

        public string TrailParticleId => _prefab.Id;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new System.ArgumentNullException(nameof(resolver));
        }

        public SoundEffect Create() =>
            _resolver.Instantiate(_prefab);
    }
}