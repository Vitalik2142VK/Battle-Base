using BattleBase.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileFactory : MonoBehaviour, IFactory<Projectile>
    {
        [SerializeField] private Projectile _projectilePrefab;

        private IObjectResolver _resolver;

        public string ProjectileId => _projectilePrefab.Id;

        [Inject]
        public void Construct(IObjectResolver resolver)
        {
            _resolver = resolver ?? throw new System.ArgumentNullException(nameof(resolver));
        }

        public Projectile Create() =>
            _resolver.Instantiate(_projectilePrefab);
    }
}