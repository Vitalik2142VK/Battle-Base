using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCreatorView : MonoBehaviour, IMaterialCreatorView
    {
        [SerializeField][SerializeIterface(typeof(IMaterialCreatorView))] private GameObject[] _viewComponents;

        private List<IMaterialCreatorView> _components;

        public void Init(IMaterialCreatorEvents materialCreatorEvents)
        {
            _components = new List<IMaterialCreatorView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IMaterialCreatorView>();

                foreach (var component in components)
                    component.Init(materialCreatorEvents);

                _components.AddRange(components);
            }
        }
    }
}