using UnityEngine;

namespace BattleBase.Core
{
    public class StaticPool<T> : IPool<T>
        where T : MonoBehaviour, IPoolable<T>
    {
        private readonly Pool<T> _pool;

        public StaticPool(IFactory<T> factory)
        {
            _pool = new Pool<T>(factory);
        }

        public bool TryGive(out T element) =>
            _pool.TryGive(out element);
    }
}