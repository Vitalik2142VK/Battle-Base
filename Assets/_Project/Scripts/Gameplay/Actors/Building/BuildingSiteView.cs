using UnityEngine;

namespace BattleBase.Gameplay.Actors.Building
{
    public class BuildingSiteView : MonoBehaviour
    {
        [SerializeField][SerializeIterface(typeof(IBuildingSiteEvents))] private GameObject _buildingSite;
        [SerializeField][SerializeIterface(typeof(IBuildingSiteView))] private GameObject[] _buildingSiteViews;

        private IBuildingSiteEvents _events;

        private void Awake()
        {
            _events = _buildingSite.GetComponent<IBuildingSiteEvents>();
        }

        private void Start()
        {
            foreach (var gameObject in _buildingSiteViews)
            {
                IBuildingSiteView view = gameObject.GetComponent<IBuildingSiteView>();
                view.Init(_events);
            }
        }
    }
}