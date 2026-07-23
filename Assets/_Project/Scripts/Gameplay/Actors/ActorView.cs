using BattleBase.Gameplay.Actors.Spawn;
using BattleBase.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors 
{
    public class ActorView : MonoBehaviour, IActorView
    {
        [SerializeField][SerializeIterface(typeof(IActorViewComponent))] private GameObject[] _viewComponents;

        private Dictionary<Type, IActorViewComponent> _components;
        private Transform _transform;

        public Vector3 Position => _transform.position;

        public void Init()
        {
            _transform = transform;
            _components = new Dictionary<Type, IActorViewComponent>();

            AddActorViewComponents(gameObject.GetComponents<IActorViewComponent>());

            foreach (var gameObject in _viewComponents)
                AddActorViewComponents(gameObject.GetComponents<IActorViewComponent>());
        }

        public void SetActive(bool isActive) => 
            gameObject.SetActive(isActive);

        public void SetSpawnData(ISpawnPoint spawnData)
        {
            if (spawnData == null)
                throw new ArgumentNullException(nameof(spawnData));

            _transform.SetPositionAndRotation(spawnData.SpawnPosition, spawnData.SpawnRotation);
        }

        public bool TryGetViewComponent<T>(out T component) where T : class, IActorViewComponent
        {
            if (_components.TryGetValue(typeof(T), out var value))
            {
                component = (T)value;

                return true;
            }

            component = null;

            return false;
        }

        private void AddActorViewComponents(IActorViewComponent[] components)
        {
            foreach (var component in components)
            {
                Type heir = TypeTools.FindDerivedInterface<IActorViewComponent>(component);

                _components[heir] = component;
            }
        }
    }
}
