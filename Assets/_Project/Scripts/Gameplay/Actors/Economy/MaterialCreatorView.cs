using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCreatorView : MonoBehaviour, IMaterialCreatorView
    {
        [SerializeField][SerializeIterface(typeof(IMaterialCreatorView))] private GameObject[] _viewComponents;

        private List<IMaterialCreatorView> _components;

        private void Awake()
        {
            _components = new List<IMaterialCreatorView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IMaterialCreatorView>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IMaterialCreatorEvents materialCreatorEvents)
        {
            foreach (var component in _components)
                component.Init(materialCreatorEvents);
        }
    }
}