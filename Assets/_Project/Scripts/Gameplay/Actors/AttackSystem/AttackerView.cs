using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerView : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IAttackerViewComponent))] private GameObject[] _viewComponents;

        private List<IAttackerViewComponent> _components;

        private void Awake()
        {
            _components = new List<IAttackerViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IAttackerViewComponent>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IAttackNotifier attackNotifier)
        {
            foreach (var component in _components)
                component.Init(attackNotifier);
        }
    }
}