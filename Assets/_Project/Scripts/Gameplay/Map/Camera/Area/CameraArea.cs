using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(Collider))]
    public class CameraArea : MonoBehaviour, ICameraArea
    {
        [SerializeField] private Color _areaBorders = Color.blue;
        [SerializeField] private Color _gizmoOvershootBorders = Color.red;
        [SerializeField][Min(0)] private float _overshoot = 0.5f;
        [SerializeField][Range(0f, 1f)] private float _resistance = 0.8f;
        [SerializeField] private bool _isImmutable = true;

        private Collider _collider;
        private Bounds _cachedColliderBounds;
        private Bounds _cachedOvershootBounds;
        private bool _isCacheValid = false;

        public float Resistance => _resistance;

        public float Overshoot => _overshoot;

        public float PlaneY => ColliderBounds.center.y;

        public Bounds ColliderBounds
        {
            get
            {
                if (_collider == null)
                {
                    FindComponents();

                    if (_collider == null)
                        throw new InvalidOperationException($"{nameof(CameraArea)} has no Collider component. Make sure the GameObject has a Collider.");
                }


                if (_isImmutable && _isCacheValid)
                    return _cachedColliderBounds;

                Refresh();

                return _cachedColliderBounds;
            }
        }

        public Bounds OvershootBounds
        {
            get
            {
                if (_collider == null)
                {
                    FindComponents();

                    if (_collider == null)
                        throw new InvalidOperationException($"{nameof(CameraArea)} has no Collider component. Make sure the GameObject has a Collider.");
                }

                if (_isImmutable && _isCacheValid)
                    return _cachedOvershootBounds;

                Refresh();

                return _cachedOvershootBounds;
            }
        }

        private void OnDrawGizmos()
        {
            if (_collider == null)
                FindComponents();

            if (_collider == null)
                return;

            Refresh();

            Gizmos.color = _areaBorders;
            Gizmos.DrawWireCube(_cachedColliderBounds.center, _cachedColliderBounds.size);

            Vector3 overshootSize = _cachedColliderBounds.size + new Vector3(_overshoot * 2f, 0f, _overshoot * 2f);
            Gizmos.color = _gizmoOvershootBorders;
            Gizmos.DrawWireCube(_cachedColliderBounds.center, overshootSize);
        }

        private void Awake() =>
            FindComponents();

        private void Start() =>
            Refresh();

        private void Refresh()
        {
            if (_collider == null)
                return;

            _cachedColliderBounds = _collider.bounds;
            Vector3 overshootSize = _cachedColliderBounds.size + new Vector3(_overshoot * 2f, 0f, _overshoot * 2f);
            _cachedOvershootBounds = new Bounds(_cachedColliderBounds.center, overshootSize);
            _isCacheValid = true;
        }

        private void FindComponents() =>
            _collider = GetComponent<Collider>();
    }
}