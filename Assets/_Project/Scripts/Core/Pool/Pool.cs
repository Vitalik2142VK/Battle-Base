using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Core
{
    public class Pool<T> : IPool<T>
        where T : MonoBehaviour, IPoolable<T>
    {
        private readonly IFactory<T> _factory;
        private readonly Stack<T> _elements;
        private readonly Transform _container;
        private readonly int _size;

        private int _count;

        public Pool(IFactory<T> factory, Transform container = null, int size = int.MaxValue)
        {
            _elements = new Stack<T>();

            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _container = container;
            _size = size;
        }

        public bool TryGive(out T element)
        {
            element = null;

            if (_elements.Count == 0 && _count >= _size)
                return false;

            element = _elements.Count > 0 ? _elements.Pop() : Create();
            element.Deactivated += Return;

            return true;
        }

        private void Return(T element)
        {
            if (element == null)
                return;

            element.Deactivated -= Return;
            element.transform.SetParent(_container);
            element.gameObject.SetActive(false);
            _elements.Push(element);
        }

        private T Create()
        {
            _count++;
            T element = _factory.Create();

            return element;
        }
    }
}