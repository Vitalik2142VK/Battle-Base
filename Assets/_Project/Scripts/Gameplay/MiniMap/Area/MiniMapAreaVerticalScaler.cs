using System;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;
using static UnityEngine.RectTransform;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapAreaVerticalScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;

        private ICameraOrientationAdapter _orientationAdapter;
        private ICameraAreaService _cameraAreaService;

        [Inject]
        public void Construct(
            ICameraOrientationAdapter orientationAdapter,
            ICameraAreaService cameraArea)
        {
            _orientationAdapter = orientationAdapter ?? throw new ArgumentNullException(nameof(orientationAdapter));
            _cameraAreaService = cameraArea ?? throw new ArgumentNullException(nameof(cameraArea));
        }

        private void OnEnable()
        {
            _cameraAreaService.Changed += OnAreaChanged;
            _orientationAdapter.Changed += OnAreaChanged;
            OnAreaChanged();
        }

        private void OnDisable()
        {
            _cameraAreaService.Changed -= OnAreaChanged;
            _orientationAdapter.Changed -= OnAreaChanged;
        }

        private void OnAreaChanged()
        {
            Bounds colliderBounds = _cameraAreaService.AreaBounds;
            float worldWidth = colliderBounds.size.x;
            float worldHeight = colliderBounds.size.z;

            float aspectRatio = worldHeight / worldWidth;
            float miniMapWidth = _miniMapArea.Rect.width;
            float miniMapHeight = miniMapWidth * aspectRatio;

            _miniMapArea.SetSizeWithCurrentAnchors(Axis.Vertical, miniMapHeight);
        }
    }
}