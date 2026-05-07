using System;
using System.Collections.Generic;
using BattleBase.DI;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapVisualizer : MonoBehaviour, IInjectable
    {
        [SerializeField] private MiniMapArea _mapArea;
        [SerializeField] private IconMapObject _prefab;
        [SerializeField] private RectTransform _context;

        private readonly Dictionary<IMiniMapTrackable, IconMapObject> _icons = new();

        private IMiniMapObjectRegistry _objectRegistry;
        private ICameraAreaService _areaService;
        private IIconSizeCalculator _calculator;

        [Inject]
        public void Construct(IMiniMapObjectRegistry objectRegistry, ICameraAreaService areaService)
        {
            _objectRegistry = objectRegistry ?? throw new ArgumentNullException(nameof(objectRegistry));
            _areaService = areaService ?? throw new ArgumentNullException(nameof(areaService));

            _calculator = _mapArea.Orientation == ScreenOrientationType.Portrait
                ? new PortraitIconSizeCalculator()
                : new LandscapeIconSizeCalculator();
        }

        private void OnEnable()
        {
            _objectRegistry.Changed += OnRegistryChanged;
            _areaService.Changed += OnAreaChanged;
            _mapArea.SizeChanged += OnMiniMapSizeChanged;
            RebuildAllIcons();
        }

        private void OnDisable()
        {
            _objectRegistry.Changed -= OnRegistryChanged;
            _areaService.Changed -= OnAreaChanged;
            _mapArea.SizeChanged -= OnMiniMapSizeChanged;
            ClearAllIcons();
        }

        private void Update()
        {
            foreach (var kvp in _icons)
            {
                if (kvp.Key == null)
                    continue;

                UpdateIconPositionAndRotation(kvp.Key, kvp.Value);
            }
        }

        private void OnRegistryChanged() => 
            RebuildAllIcons();

        private void OnAreaChanged() => 
            RecalculateAllIconSizes();

        private void OnMiniMapSizeChanged() => 
            RecalculateAllIconSizes();

        private void RebuildAllIcons()
        {
            ClearAllIcons();

            if (_objectRegistry is not MiniMapObjectRegistry registry)
                return;

            foreach (IMiniMapTrackable trackable in registry.Trackables)
                CreateIcon(trackable);
        }

        private void ClearAllIcons()
        {
            foreach (var kvp in _icons)
                if (kvp.Value != null) Destroy(kvp.Value.gameObject);

            _icons.Clear();
        }

        private void RecalculateAllIconSizes()
        {
            foreach (var kvp in _icons)
                UpdateIconSize(kvp.Value, kvp.Key.WorldSize);
        }

        private void CreateIcon(IMiniMapTrackable trackable)
        {
            IconMapObject icon = Instantiate(_prefab, _context);
            icon.SetColor(trackable.Color);
            _icons[trackable] = icon;

            UpdateIconSize(icon, trackable.WorldSize);
            UpdateIconPositionAndRotation(trackable, icon);
        }

        private void UpdateIconSize(IconMapObject icon, Vector2 worldSize)
        {
            Bounds bounds = _areaService.AreaBounds;
            Rect rect = _mapArea.Rect;

            if (rect.width <= 0f || rect.height <= 0f || bounds.size.x <= 0f || bounds.size.z <= 0f)
                return;

            Vector2 size = _calculator.GetIconSize(worldSize, bounds, rect);
            icon.SetSize(size);
        }

        private void UpdateIconPositionAndRotation(IMiniMapTrackable trackable, IconMapObject icon)
        {
            Bounds bounds = _areaService.AreaBounds;
            Rect rect = _mapArea.Rect;

            if (rect.width <= 0f || rect.height <= 0f || bounds.size.x <= 0f || bounds.size.z <= 0f)
                return;

            Vector2 uiPosition = _calculator.WorldToMiniMapPosition(trackable.WorldPosition, bounds, rect);            
            icon.SetAnchoredPosition(uiPosition);
            icon.SetRotation(trackable.RotationY);
        }
    }
}