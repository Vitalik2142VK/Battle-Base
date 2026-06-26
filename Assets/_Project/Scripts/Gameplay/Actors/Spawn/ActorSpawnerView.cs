using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Spawn
{
    public class ActorSpawnerView : MonoBehaviour, IActorSpawnerView
    {
        [SerializeField][SerializeIterface(typeof(IActorSpawnerView))] private GameObject[] _viewComponents;

        private List<IActorSpawnerView> _components;

        public void Init(IActorSpawnerEvents events)
        {
            _components = new List<IActorSpawnerView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IActorSpawnerView>();

                foreach (var component in components)
                    component.Init(events);

                _components.AddRange(components);
            }
        }
    }
}
