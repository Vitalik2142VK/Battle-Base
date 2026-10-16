using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerView : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IAttackerViewComponent))] private GameObject[] _viewComponents;

        private List<IAttackerViewComponent> _components;

        public void Init(IAttackNotifier attackNotifier)
        {
            _components = new List<IAttackerViewComponent>();

            Debug.Log($"AttackerView._components.Count = {_components.Count}");

            foreach (var gameObject in _viewComponents)
            {
                Debug.Log($"gameObject.name = {gameObject.name}");

                var components = gameObject.GetComponents<IAttackerViewComponent>();

                Debug.Log($"componentsCountIAttackerViewComponent = {components.Length}");

                if (components.Length == 0)
                    throw new System.Exception();

                foreach (var component in components)
                    component.Init(attackNotifier);

                _components.AddRange(components);
            }
        }
    }
}