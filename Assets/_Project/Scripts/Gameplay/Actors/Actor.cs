using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class Actor : IActor, IPoolable<Actor>
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private IDamagebleEvents _damagebleEvents;

        public event Action<Actor> Deactivated;

        public Actor(
            Dictionary<Type, IActorComponent> components, 
            IActorView view, 
            IActorData actorData,
            IDamagebleEvents damagebleEvent)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            if (components.Count == 0)
                throw new ArgumentException($"{nameof(components)} cannot be empty");

            _components = components;
            _damagebleEvents = damagebleEvent ?? throw new ArgumentNullException(nameof(damagebleEvent));

            View = view ?? throw new ArgumentNullException(nameof(view));
            Data = actorData ?? throw new ArgumentNullException(nameof(actorData));
        }

        public IActorData Data { get; }

        public IActorView View { get; }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                component = (T)value;

                return true;
            }

            component = null;

            return false;
        }

        public void Enable()
        {
            _damagebleEvents.Destroyed += OnDestroy;

            View.SetActive(true);

            foreach (var component in _components.Values)
                component.Reset();
        }

        public void Disable()
        {
            _damagebleEvents.Destroyed -= OnDestroy;

            View.SetActive(false);
        }

        private void OnDestroy()
        {
            Deactivated?.Invoke(this);
        }
    }
}
