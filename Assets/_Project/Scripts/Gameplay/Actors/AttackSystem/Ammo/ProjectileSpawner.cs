using BattleBase.Core;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem.Ammo
{
    public class ProjectileSpawner : MonoBehaviour, IProjectileSpawner
    {
        [SerializeField] private List<ProjectileFactory> _factories;
        [SerializeField] private Transform _container;

        private Dictionary<string, Pool<Projectile>> _pools;

        private void OnValidate()
        {
            if (_container == null)
                _container = transform;
        }

        private void Awake()
        {
            _pools = new Dictionary<string, Pool<Projectile>>();

            foreach (var factory in _factories)
            {
                string ProjectileId = factory.ProjectileId;
                GameObject container = new($"{ProjectileId}Container");
                Pool<Projectile> pool = new(factory, container.transform);
                container.transform.SetParent(_container);
                container.isStatic = true;
                _pools.Add(factory.ProjectileId, pool);
            }
        }

        public IProjectile Spawn(string missileId)
        {
            if (string.IsNullOrEmpty(missileId))
                throw new ArgumentException($"{nameof(missileId)} cannot be null or empty");

            if (_pools.ContainsKey(missileId) == false)
                throw new InvalidOperationException($"{_pools} don't contains key '{missileId}'");

            var pool = _pools[missileId];

            if (pool.TryGive(out Projectile missile) == false)
                throw new InvalidOperationException($"There are too many objects, expand the pool");

            missile.gameObject.SetActive(true);

            return missile;
        }
    }
}