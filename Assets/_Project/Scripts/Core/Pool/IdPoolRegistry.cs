using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Core
{
    public class IdPoolRegistry<TObject, TFactory>
        where TObject : MonoBehaviour, IPoolable<TObject>
        where TFactory : IFactory<TObject>
    {
        private readonly Dictionary<string, Pool<TObject>> _pools;

        public IdPoolRegistry(
            IEnumerable<TFactory> factories,
            Transform container, Func<TFactory, string> idSelector,
            int size = int.MaxValue)
        {
            if (factories == null)
                throw new ArgumentNullException(nameof(factories));

            _pools = new();

            foreach (var factory in factories)
            {
                string id = idSelector(factory);

                var poolContainer = new GameObject($"{id}Container");
                poolContainer.transform.SetParent(container);
                poolContainer.isStatic = true;

                Pool<TObject> pool = new(factory, poolContainer.transform, size);
                _pools.Add(id, pool);
            }
        }

        public TObject Give(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"{nameof(id)} cannot be null or empty");

            if (_pools.TryGetValue(id, out var pool) == false)
                throw new InvalidOperationException($"{_pools} don't contains key '{id}'");

            if (pool.TryGive(out TObject obj) == false)
                throw new InvalidOperationException($"Pool '{id}' is exhausted");

            obj.gameObject.SetActive(true);

            return obj;
        }

        public bool TryGive(string id, out TObject obj)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException($"{nameof(id)} cannot be null or empty");

            if (_pools.TryGetValue(id, out var pool) == false)
                throw new InvalidOperationException($"{_pools} don't contains key '{id}'");

            if (pool.TryGive(out obj))
            {
                obj.gameObject.SetActive(true);

                return true;
            }

            return false;
        }
    }
}