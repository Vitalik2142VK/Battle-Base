using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(Collider))]
    public class CameraArea : MonoBehaviour, ICameraArea
    {
        private const float OvershootScaleFactor = 2f;

        [SerializeField][Min(0)] private float _resistanceFadeDistance = 0.5f;
        [SerializeField][Range(0f, 1f)] private float _resistance = 0.8f;
        [SerializeField] private bool _areBoundsStatic = true;

        private Collider _collider;
        private Bounds _cachedColliderBounds;
        private Bounds _cachedOvershootBounds;
        private bool _isCacheValid = false;

        public event Action Changed;

        public float Resistance => _resistance;

        public float ResistanceFadeDistance => _resistanceFadeDistance;

        public float GroundPlaneY => ColliderBounds.center.y;

        public Bounds ColliderBounds
        {
            get
            {
                EnsureComponents();

                if (_areBoundsStatic && _isCacheValid)
                    return _cachedColliderBounds;

                if (_isCacheValid == false)
                    Refresh();

                return _cachedColliderBounds;
            }
        }

        public Bounds OvershootBounds
        {
            get
            {
                EnsureComponents();

                if (_areBoundsStatic && _isCacheValid)
                    return _cachedOvershootBounds;

                if (_isCacheValid == false)
                    Refresh();

                return _cachedOvershootBounds;
            }
        }

        private void Awake() =>
            FindComponents();

        private void Start() =>
            Refresh();

        public void Refresh()
        {
            if (_collider == null)
                return;

            _cachedColliderBounds = _collider.bounds;
            Vector3 overshootSize = _cachedColliderBounds.size + new Vector3(_resistanceFadeDistance * OvershootScaleFactor, 0f, _resistanceFadeDistance * OvershootScaleFactor);
            _cachedOvershootBounds = new Bounds(_cachedColliderBounds.center, overshootSize);
            _isCacheValid = true;

            Changed?.Invoke();
        }

        private void EnsureComponents()
        {
            if (_collider == null)
                FindComponents();
        }

        private void FindComponents()
        {
            _collider = GetComponent<Collider>();

            if (_collider == null)
                throw new ArgumentNullException(nameof(_collider));
        }
    }
}