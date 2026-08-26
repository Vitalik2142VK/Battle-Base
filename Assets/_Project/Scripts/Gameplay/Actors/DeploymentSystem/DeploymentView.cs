using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DeploymentSystem
{
    public class DeploymentView : MonoBehaviour, IDeploymentView
    {
        [SerializeField][SerializeIterface(typeof(IDeploymentView))] private GameObject[] _viewComponents;

        private List<IDeploymentView> _components;

        public void Init(IDeploymentEvets deploymentEvets)
        {
            _components = new List<IDeploymentView>();

            foreach (var gameObject in _viewComponents)
            {
                var components = gameObject.GetComponents<IDeploymentView>();

                foreach (var component in components)
                    component.Init(deploymentEvets);

                _components.AddRange(components);
            }
        }
    }
}
