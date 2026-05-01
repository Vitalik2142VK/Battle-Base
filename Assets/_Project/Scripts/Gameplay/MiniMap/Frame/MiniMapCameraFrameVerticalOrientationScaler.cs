using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapCameraFrameVerticalOrientationScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _area;
        [SerializeField] private MiniMapCameraFrame _frame;

        private ICameraAreaService _areaService;
        private ICameraZoom _cameraZoom;
        private IFrustumProjectionService _frustumProjectionService;

        [Inject]
        public void Construct(
            ICameraAreaService areaService,
            ICameraZoom cameraZoom,
            IFrustumProjectionService projectionService)
        {
            _areaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
            _cameraZoom = cameraZoom ?? throw new ArgumentNullException(nameof(cameraZoom));
            _frustumProjectionService = projectionService ?? throw new ArgumentNullException(nameof(projectionService));
        }

        private void OnEnable()
        {
            _cameraZoom.Changed += Refresh;
            _area.SizeChanged += Refresh;
            _frustumProjectionService.Changed += Refresh;
            Refresh();
        }

        private void OnDisable()
        {
            _cameraZoom.Changed -= Refresh;
            _area.SizeChanged -= Refresh;
            _frustumProjectionService.Changed -= Refresh;
        }

        private void Refresh()
        {
            float width = CalculateFrameSide(
                _frustumProjectionService.CachedWidth,
                _areaService.AreaBounds.size.x,
                _area.Rect.width);

            float height = CalculateFrameSide(
                _frustumProjectionService.CachedHeight,
                _areaService.AreaBounds.size.z,
                _area.Rect.height);

            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            _frame.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        private float CalculateFrameSide(float frustumSize, float worldAreaSize, float mapAreaSize)
        {
            float factor = frustumSize / worldAreaSize;

            return mapAreaSize * factor;
        }
    }
}