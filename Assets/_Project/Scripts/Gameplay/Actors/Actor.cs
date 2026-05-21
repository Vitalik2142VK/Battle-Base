using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors
{
    public class Actor : IActor, IPoolable<Actor>
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private readonly List<IUpdateable> _updateableComponents;
        private readonly IDestroyableEvents _damagebleEvents;

        private TeamType _teamType;

        public event Action<Actor> Deactivated;

        public Actor(
            Dictionary<Type, IActorComponent> components, 
            IActorView view, 
            IActorData actorData,
            IDestroyableEvents damagebleEvent)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            if (components.Count == 0)
                throw new ArgumentException($"{nameof(components)} cannot be empty");

            _components = components;
            _damagebleEvents = damagebleEvent ?? throw new ArgumentNullException(nameof(damagebleEvent));
            _teamType = TeamType.None;

            View = view ?? throw new ArgumentNullException(nameof(view));
            Data = actorData ?? throw new ArgumentNullException(nameof(actorData));

            _updateableComponents = new List<IUpdateable>();

            foreach (var component in _components.Values)
            {
                if (component is IUpdateable componentUpdateable)
                    _updateableComponents.Add(componentUpdateable);
            }
        }

        public TeamType TeamType => _teamType;

        public IActorData Data { get; }

        public IActorView View { get; }

        public bool IsEnabled { get; private set; }

        public void Update(float delta)
        {
            if (_updateableComponents.Count == 0)
                return;

            for (int i = 0; i < _updateableComponents.Count; i++)
                _updateableComponents[i].Update(delta);
        }

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
            IsEnabled = true;

            View.SetActive(true);

            foreach (var component in _components.Values)
                component.Enable();
        }

        public void Disable()
        {
            _damagebleEvents.Destroyed -= OnDestroy;
            IsEnabled = false;

            View.SetActive(false);

            foreach (var component in _components.Values)
                component.Disable();
        }

        public void SetTeam(TeamType teamType) => _teamType = teamType;

        private void OnDestroy()
        {
            Deactivated?.Invoke(this);
        }
    }
}
