using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapCameraFrameVerticalOrientationScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _area;
        [SerializeField] private MiniMapCameraFrame _frame;

        private ICameraOrientationAdapter _orientationAdapter;
        private ICameraZoom _cameraZoom;
        private ICameraFrustumProjector _frustumProjector;

        private List<Vector3> _cornersBuffer = new();

        [Inject]
        public void Construct(
            ICameraOrientationAdapter orientationAdapter,
            ICameraZoom cameraZoom,
            ICameraFrustumProjector frustumProjector)
        {
            _orientationAdapter = orientationAdapter ?? throw new ArgumentNullException(nameof(orientationAdapter));
            _cameraZoom = cameraZoom ?? throw new ArgumentNullException(nameof(cameraZoom));
            _frustumProjector = frustumProjector ?? throw new ArgumentNullException(nameof(frustumProjector));
        }

        private void OnEnable()
        {
            _cameraZoom.Changed += OnZoomChanged;
            _area.SizeChanged += OnZoomChanged;
            OnZoomChanged();
        }

        private void OnDisable()
        {
            _cameraZoom.Changed -= OnZoomChanged;
            _area.SizeChanged -= OnZoomChanged;
        }

        private void OnZoomChanged()
        {
            _cornersBuffer = new(_frustumProjector.Corners);

            if (_cornersBuffer.Count < 4)
                return;

            float minX = float.MaxValue, maxX = float.MinValue;
            float minZ = float.MaxValue, maxZ = float.MinValue;

            foreach (Vector3 corner in _cornersBuffer)
            {
                minX = Mathf.Min(minX, corner.x);
                maxX = Mathf.Max(maxX, corner.x);
                minZ = Mathf.Min(minZ, corner.z);
                maxZ = Mathf.Max(maxZ, corner.z);
            }

            float worldWidth = maxX - minX;
            float worldHeight = maxZ - minZ;

            if (worldWidth <= 0f)
                return;

            float current = _orientationAdapter.CurrentOrtoSize;
            float max = _orientationAdapter.MaximumOrtoSize;
            float frameAreaWidth = _area.Rect.width;
            float factor = current / max;
            float frameWidth = frameAreaWidth * factor;
            float frameHeight = frameWidth * (worldHeight / worldWidth);

            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, frameWidth);
            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, frameHeight);
        }
    }
}