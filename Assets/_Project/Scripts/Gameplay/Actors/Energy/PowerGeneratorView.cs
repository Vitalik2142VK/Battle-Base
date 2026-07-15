using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class PowerGeneratorView : MonoBehaviour, IPowerGeneratorView
    {
        [SerializeField][SerializeIterface(typeof(IPowerGeneratorView))] private GameObject[] _viewComponents;

        private List<IPowerGeneratorView> _components;

        public void Init(IPowerGeneratorNotifier powerGeneratorNotifier)
        {
            _components = new List<IPowerGeneratorView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IPowerGeneratorView>();

                foreach (var component in components)
                    component.Init(powerGeneratorNotifier);

                _components.AddRange(components);
            }
        }
    }
}
