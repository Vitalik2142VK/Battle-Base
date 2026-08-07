using UnityEngine;

namespace BattleBase.Core
{
    public class BoxArea : MonoBehaviour
    {
        [Header("Area")]
        [SerializeField] private Vector3 _center = Vector3.zero;
        [SerializeField] private Vector3 _size = Vector3.one;

        [Header("Debug")]
        [SerializeField] private bool _isDebugEnable = false;

        private Transform _transform;

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