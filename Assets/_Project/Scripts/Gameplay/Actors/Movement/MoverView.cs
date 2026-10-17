using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class MoverView : MonoBehaviour, IMoverViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IMoverViewComponent))] private GameObject[] _viewComponents;

        private List<IMoverViewComponent> _components;

        private void Awake()
        {
            _components = new List<IMoverViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IMoverViewComponent>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IMoverEvents moverEvents)
        {
            foreach (var component in _components)
                component.Init(moverEvents);
        }
    }
}
