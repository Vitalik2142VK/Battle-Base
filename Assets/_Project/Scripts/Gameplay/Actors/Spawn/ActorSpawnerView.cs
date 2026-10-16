using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerView : MonoBehaviour, IActorSpawnerView
    {
        [SerializeField][SerializeIterface(typeof(IActorSpawnerView))] private GameObject[] _viewComponents;

        private List<IActorSpawnerView> _components;

        private void Awake()
        {
            _components = new List<IActorSpawnerView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IActorSpawnerView>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IActorSpawnerNotifier notifier)
        {
            foreach (var component in _components)
                component.Init(notifier);
        }
    }
}
