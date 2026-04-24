using System;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using UnityEngine;
using VContainer;
using static UnityEngine.RectTransform;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapAreaVerticalScaler : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _miniMapArea;

        private ICameraOrientationAdapter _orientationAdapter;
        private ICameraArea _cameraArea;

        [Inject]
        public void Construct(
            ICameraOrientationAdapter orientationAdapter,
            ICameraArea cameraArea)
        {
            _orientationAdapter = orientationAdapter ?? throw new ArgumentNullException(nameof(orientationAdapter));
            _cameraArea = cameraArea ?? throw new ArgumentNullException(nameof(cameraArea));
        }

        private void OnEnable()
        {
            _cameraArea.Changed += OnAreaChanged;
            _orientationAdapter.Changed += OnAreaChanged;
            OnAreaChanged();
        }

        private void OnDisable()
        {
            _cameraArea.Changed -= OnAreaChanged;
            _orientationAdapter.Changed -= OnAreaChanged;
        }

        private void OnAreaChanged()
        {
            Bounds colliderBounds = _cameraArea.ColliderBounds;
            float worldWidth = colliderBounds.size.x;
            float worldHeight = colliderBounds.size.z;

            float aspectRatio = worldHeight / worldWidth;
            float miniMapWidth = _miniMapArea.Rect.width;
            float miniMapHeight = miniMapWidth * aspectRatio;

            _miniMapArea.SetSizeWithCurrentAnchors(Axis.Vertical, miniMapHeight);
        }
    }
}