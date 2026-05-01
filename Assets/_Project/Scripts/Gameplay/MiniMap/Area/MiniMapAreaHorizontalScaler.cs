using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapAreaHorizontalScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;

        private ICameraAreaService _cameraAreaService;

        [Inject]
        public void Construct(ICameraAreaService areaService) =>
            _cameraAreaService = areaService ?? throw new ArgumentNullException(nameof(areaService));

        private void OnEnable()
        {
            _cameraAreaService.Changed += UpdateHorizontalSize;
            UpdateHorizontalSize();
        }

        private void OnDisable() =>
            _cameraAreaService.Changed -= UpdateHorizontalSize;

        private void UpdateHorizontalSize()
        {
            Bounds colliderBounds = _cameraAreaService.AreaBounds;
            float worldWidth = colliderBounds.size.z;
            float worldHeight = colliderBounds.size.x;

            float aspectRatio = worldWidth / worldHeight;
            float miniMapHeight = _miniMapArea.Rect.height;
            float miniMapWidth = miniMapHeight * aspectRatio;

            _miniMapArea.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, miniMapWidth);
        }
    }
}