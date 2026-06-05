using BattleBase.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class RouteStartArea : MonoBehaviour
    {
        [SerializeField] private List<Waypoint> _route;

        [Header("Area")]
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private Vector3 _size = Vector3.one;

        [Header("Debug")]
        [SerializeField][Min(0.5f)] private float _radiusWaypoint = 1f;
        [SerializeField] private bool _isDebugEnable = false;

        private Transform _transform;

        public IEnumerable<IWaypoint> Route => _route;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDrawGizmosSelected()
        {
            if (_isDebugEnable == false)
                return;

            Gizmos.color = Color.green;

            Matrix4x4 old = Gizmos.matrix;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(_center, _size);
            Gizmos.matrix = old;

            Gizmos.color = Color.red;

            foreach (var waypoint in _route)
                Gizmos.DrawSphere(waypoint.transform.position, _radiusWaypoint);
        }

        public bool HasInArea(Vector3 position)
        {
            Vector3 localPoint = _transform.InverseTransformPoint(position);

            float halfValue = 0.5f;
            Vector3 min = _center - _size * halfValue;
            Vector3 max = _center + _size * halfValue;

            return localPoint.x >= min.x && localPoint.x <= max.x &&
                   localPoint.y >= min.y && localPoint.y <= max.y &&
                   localPoint.z >= min.z && localPoint.z <= max.z;
        }
    }
}