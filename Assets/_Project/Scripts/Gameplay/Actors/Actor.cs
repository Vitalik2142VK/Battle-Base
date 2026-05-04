using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class Actor : IActor
    {
        private readonly Dictionary<Type, IActorComponent> _components;

        public Actor()
        {
            _components = new Dictionary<Type, IActorComponent>();
        }

        public void Add<T>(T component) where T : class, IActorComponent
        {
            if (_components.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Component {typeof(T)} already exists");

            _components[typeof(T)] = component;
        }

        public bool TryGet<T>(out T component) where T : class, IActorComponent
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                component = (T)value;

                return true;
            }

            component = null;

            return false;
        }
    }
}
