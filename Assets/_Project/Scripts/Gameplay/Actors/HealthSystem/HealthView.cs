using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.HealthSystem
{
    public class HealthView : MonoBehaviour, IHealthViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IHealthViewComponent))] private GameObject[] _viewComponents;

        private List<IHealthViewComponent> _components;

        private void Awake()
        {
            _components = new List<IHealthViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IHealthViewComponent>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IHealthEvents healthEvents)
        {
            foreach (var component in _components)
                component.Init(healthEvents);
        }
    }
}