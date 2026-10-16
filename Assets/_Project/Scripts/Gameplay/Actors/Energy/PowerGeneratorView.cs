using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGeneratorView : MonoBehaviour, IPowerGeneratorView
    {
        [SerializeField][SerializeIterface(typeof(IPowerGeneratorView))] private GameObject[] _viewComponents;

        private List<IPowerGeneratorView> _components;

        private void Awake()
        {
            _components = new List<IPowerGeneratorView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IPowerGeneratorView>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IPowerGeneratorNotifier powerGeneratorNotifier)
        {
            foreach (var component in _components)
                component.Init(powerGeneratorNotifier);
        }
    }
}
