using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public class Actor : IActor, IPoolable<Actor>
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private readonly IUpdateableController _updateableController;
        private readonly IDestroyableEvents _damagebleEvents;

        public event Action<Actor> Deactivated;
        public event Action<Color> ColorChanged;

        public Actor(
            Dictionary<Type, IActorComponent> components,
            IActorView view,
            IActorData actorData,
            IDestroyableEvents damagebleEvent,
            IUpdateableController updateableController = null)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            if (components.Count == 0)
                throw new ArgumentException($"{nameof(components)} cannot be empty");

            _components = components;
            _damagebleEvents = damagebleEvent ?? throw new ArgumentNullException(nameof(damagebleEvent));
            _updateableController = updateableController ?? new UpdateableController(_components.Values);
            TeamType = TeamType.None;

            View = view ?? throw new ArgumentNullException(nameof(view));
            Data = actorData ?? throw new ArgumentNullException(nameof(actorData));
        }

        public IActorData Data { get; }

        public IActorView View { get; }

        public TeamType TeamType { get; private set; }

        public bool IsEnabled { get; private set; }

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

        public void Update(float delta) =>
            _updateableController.Update(delta);

        public void SetTeam(TeamType teamType) =>
            TeamType = teamType;

        public void ChangeColor(Color color) =>
            ColorChanged?.Invoke(color);
        public void SetSpawnData(ISpawnData spawnData) =>
            View.SetSpawnData(spawnData);

        private void OnDestroy()
        {
            Deactivated?.Invoke(this);
        }
    }
}
