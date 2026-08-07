using BattleBase.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    [RequireComponent(typeof(BoxArea))]
    public class RouteStartArea : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _route;

        [Header("Debug")]
        [SerializeField][Min(0.5f)] private float _radiusWaypoint = 1f;
        [SerializeField] private bool _isShowWaypoits = false;

        private BoxArea _boxArea;

        private void Awake()
        {
            _boxArea = GetComponent<BoxArea>();
        }

        public IEnumerable<IWaypoint> Route => _route;

        private void OnDrawGizmosSelected()
        {
            if (_isShowWaypoits == false)
                return;

            Gizmos.color = Color.red;

            foreach (var waypoint in _route)
                Gizmos.DrawSphere(waypoint.transform.position, _radiusWaypoint);
        }

        public bool HasInArea(Vector3 position) =>
            _boxArea.HasInArea(position);
    }
}
