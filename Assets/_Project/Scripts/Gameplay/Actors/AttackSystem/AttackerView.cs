using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.AttackSystem
{
    public class AttackerView : MonoBehaviour, IAttackerViewComponent
    {
        [SerializeField][SerializeIterface(typeof(IAttackerViewComponent))] private GameObject[] _viewComponents;

        private List<IAttackerViewComponent> _components;

        public void Init(IAttackEvents attackEvents)
        {
            _components = new List<IAttackerViewComponent>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IAttackerViewComponent>();

                foreach (var component in components)
                    component.Init(attackEvents);

                _components.AddRange(components);
            }
        }
    }
}