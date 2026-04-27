using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapCameraFramePositionApplier : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;
        [SerializeField] private MiniMapCameraFrame _cameraFrame;

        private ICameraAreaService _cameraAreaService;
        private IFrustumProjectionService _frustumProjectionService;

        [Inject]
        public void Construct(
            ICameraAreaService cameraAreaService,
            IFrustumProjectionService frustumProjectionService)
        {
            _cameraAreaService = cameraAreaService ?? throw new ArgumentNullException(nameof(cameraAreaService));
            _frustumProjectionService = frustumProjectionService ?? throw new ArgumentNullException(nameof(frustumProjectionService));
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
            Vector3 worldCenter = _frustumProjectionService.ProjectedCenter;
            Bounds bounds = _cameraAreaService.AreaBounds;

            float normalizedX = (worldCenter.x - bounds.min.x) / bounds.size.x;
            float normalizedZ = (worldCenter.z - bounds.min.z) / bounds.size.z;

            Rect miniMapRect = _miniMapArea.Rect;
            float x = (normalizedX - 0.5f) * miniMapRect.width;
            float y = (normalizedZ - 0.5f) * miniMapRect.height;

            _cameraFrame.SetAnchoredPosition(new Vector2(x, y));
        }
    }
}