using BattleBase.Core;
using BattleBase.Gameplay.Actors.Spawn;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileSpawner : MonoBehaviour, IProjectileSpawner
    {
        [SerializeField] private List<ProjectileFactory> _factories;
        [SerializeField] private Transform _container;

        private IdPoolRegistry<Projectile, ProjectileFactory> _poolRegistry;

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
            _poolRegistry = new IdPoolRegistry<Projectile, ProjectileFactory>(
                _factories,
                _container,
                factory => factory.ProjectileId);
        }

        public IProjectile Spawn(string missileId) =>
            _poolRegistry.Spawn(missileId);
    }
}