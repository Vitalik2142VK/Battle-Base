using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class DeploymentView : MonoBehaviour, IDeploymentView
    {
        [SerializeField][SerializeIterface(typeof(IDeploymentView))] private GameObject[] _viewComponents;

        private List<IDeploymentView> _components;

        private void Awake()
        {
            _components = new List<IDeploymentView>();

            foreach (var component in _viewComponents)
            {
                var components = gameObject.GetComponents<IDeploymentView>();

                foreach (var componentView in components)
                    _components.Add(componentView);
            }
        }

        public void Init(IDeploymentEvets deploymentEvets)
        {
            foreach (var component in _components)
                component.Init(deploymentEvets);
        }
    }
}
