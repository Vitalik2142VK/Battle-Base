using System;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraArea : MonoBehaviour, ICameraArea
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField][Min(0)] private float _resistanceFadeDistance = 0.5f;
        [SerializeField][Range(0f, 1f)] private float _resistance = 0.8f;

        [Header("Debug")]
        [SerializeField] private bool _enabled;

        public event Action Changed;

        public float Resistance => _resistance;

        public float ResistanceFadeDistance => _resistanceFadeDistance;

        public bool Enabled => _enabled;

        public BoxCollider Collider
        {
            get
            {
                EnsureComponents();

                return _collider;
            }
        }

        private void OnValidate() =>
            InvokeChange();

        private void Start()
        {
            EnsureComponents();
            InvokeChange();
        }

        private void EnsureComponents()
        {
            if (_collider != null)
                return;

            _collider = GetComponent<BoxCollider>();

            if (_collider == null)
                throw new ArgumentNullException(nameof(_collider));
        }

        private void InvokeChange() =>
            Changed?.Invoke();
    }
}