using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverView : MonoBehaviour, IImproverViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IImproverViewComponent))] private GameObject[] _viewComponents;

        private List<IImproverViewComponent> _components;

        private void Awake()
        {
            _components = new List<IImproverViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IImproverViewComponent>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IImproverINotifier improverNotifier)
        {
            foreach (var component in _components)
                component.Init(improverNotifier);
        }
    }
}