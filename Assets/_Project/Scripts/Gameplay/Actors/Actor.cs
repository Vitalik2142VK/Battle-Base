using BattleBase.Core;
using BattleBase.Gameplay.Actors.DamageSystem;
using BattleBase.Gameplay.Actors.Movement;
using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public class Actor : IActor, IPoolable<Actor>
    {
        private readonly Dictionary<Type, IActorComponent> _components;
        private readonly IUpdateableController _updateableController;
        private readonly IDestroyComponent _destroyComponent;

        public event Action<Actor> Deactivated;
        public event Action<Color> ColorChanged;

        public Actor(
            Dictionary<Type, IActorComponent> components,
            IActorView view,
            IActorData actorData,
            IDestroyComponent destroyComponent,
            IUpdateableController updateableController = null)
        {
            if (components == null)
                throw new ArgumentNullException(nameof(components));

            if (components.Count == 0)
                throw new ArgumentException($"{nameof(components)} cannot be empty");

            _components = components;
            _destroyComponent = destroyComponent ?? throw new ArgumentNullException(nameof(destroyComponent));
            _updateableController = updateableController ?? new UpdateableController(_components.Values);

            View = view ?? throw new ArgumentNullException(nameof(view));
            Data = actorData ?? throw new ArgumentNullException(nameof(actorData));

            TeamType = TeamType.None;
            IsStatic = _components.ContainsKey(typeof(IMover)) == false;
        }

        public IActorData Data { get; }

        public IActorView View { get; }

        public IActorPosition Position => View;

        public TeamType TeamType { get; private set; }

        public bool IsEnabled { get; private set; }

        public bool IsStatic { get; }

        public void Enable()
        {
            _destroyComponent.Destroyed += OnDestroy;
            IsEnabled = true;

            View.SetActive(true);

            foreach (var component in _components.Values)
                component.Enable();
        }

        public void Disable()
        {
            _destroyComponent.Destroyed -= OnDestroy;
            IsEnabled = false;

            foreach (var component in _components.Values)
                component.Disable();

            View.SetActive(false);
        }

        public bool TryGetComponent<T>(out T component) where T : class, IActorComponent
        {
            if (_components.TryGetValue(typeof(T), out var exact))
            {
                component = (T)exact;

                return true;
            }

            foreach (var value in _components.Values)
            {
                if (value is T type)
                {
                    component = type;

                    return true;
                }
            }

            component = null;

            return false;
        }

        public void AddComponent<T>(T component) where T : class, IActorComponent
        {
            if (component == null)
                throw new ArgumentNullException(nameof(component));

            Type heir = TypeTools.FindDerivedInterface<IActorComponent>(component);

            _components[heir] = component;
            _updateableController.AddComponent(component);
        }

        public void Update(float delta) =>
            _updateableController.Update(delta);

        public void SetTeam(TeamType teamType) =>
            TeamType = teamType;

        public void ChangeColor(Color color) =>
            ColorChanged?.Invoke(color);

        public void SetSpawnData(ISpawnPoint spawnData) =>
            View.SetSpawnData(spawnData);

        private void OnDestroy()
        {
            Deactivated?.Invoke(this);
        }
    }
}
