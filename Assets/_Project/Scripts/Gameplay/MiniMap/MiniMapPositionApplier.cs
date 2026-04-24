using System;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapPositionApplier : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;
        [SerializeField] private MiniMapCameraFrame _cameraFrame;

        private ICameraArea _cameraArea;
        private ICameraFrustumProjector _cameraFrustumProjector;

        [Inject]
        public void Construct(
            ICameraArea cameraArea, 
            ICameraFrustumProjector cameraFrustumProjector)
        {
            _cameraArea = cameraArea ?? throw new ArgumentNullException(nameof(cameraArea));
            _cameraFrustumProjector = cameraFrustumProjector ?? throw new ArgumentNullException(nameof(cameraFrustumProjector));
        }

        private void OnEnable()
        {
            _miniMapArea.SizeChanged += OnAreaChanged;
            OnAreaChanged();
        }

        private void OnDisable()
        {
            _miniMapArea.SizeChanged -= OnAreaChanged;
        }

        private void OnAreaChanged() =>
            UpdatePosition();

        private void Update()
        {
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            Vector3 worldCenter = _cameraFrustumProjector.ProjectedCenter;
            Bounds bounds = _cameraArea.ColliderBounds;

            float normalizedX = (worldCenter.x - bounds.min.x) / bounds.size.x;
            float normalizedZ = (worldCenter.z - bounds.min.z) / bounds.size.z;

            Rect miniMapRect = _miniMapArea.Rect;
            float x = (normalizedX - 0.5f) * miniMapRect.width;
            float y = (normalizedZ - 0.5f) * miniMapRect.height;

            _cameraFrame.SetAnchoredPosition(new Vector2(x, y));
        }
    }
}