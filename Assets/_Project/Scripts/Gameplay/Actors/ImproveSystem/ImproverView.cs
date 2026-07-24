using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproverView : MonoBehaviour, IImproverViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IImproverViewComponent))] private GameObject[] _viewComponents;

        private List<IImproverViewComponent> _components;

        public void Init(IImproverINotifier improverNotifier)
        {
            _components = new List<IImproverViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IImproverViewComponent>();

                foreach (var component in components)
                    component.Init(improverNotifier);

                _components.AddRange(components);
            }
        }
    }
}