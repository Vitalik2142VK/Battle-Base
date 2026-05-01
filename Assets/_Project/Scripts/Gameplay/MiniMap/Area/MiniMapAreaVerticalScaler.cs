using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapAreaVerticalScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;

        private ICameraAreaService _cameraAreaService;

        [Inject]
        public void Construct(ICameraAreaService areaService) =>
            _cameraAreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));

        private void OnEnable()
        {
            _cameraAreaService.Changed += UpdateVerticalSize;
            UpdateVerticalSize();
        }

        private void OnDisable() =>
            _cameraAreaService.Changed -= UpdateVerticalSize;

        private void UpdateVerticalSize()
        {
            Bounds colliderBounds = _cameraAreaService.AreaBounds;
            float worldWidth = colliderBounds.size.x;
            float worldHeight = colliderBounds.size.z;

            float aspectRatio = worldHeight / worldWidth;
            float miniMapWidth = _miniMapArea.Rect.width;
            float miniMapHeight = miniMapWidth * aspectRatio;

            _miniMapArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, miniMapHeight);
        }
    }
}