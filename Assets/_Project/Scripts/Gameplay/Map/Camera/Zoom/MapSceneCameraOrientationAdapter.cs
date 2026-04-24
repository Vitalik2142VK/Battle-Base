using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class MapSceneCameraOrientationAdapter : ICameraOrientationAdapter, IDisposable
    {
        private const float AspectComparisonEpsilon = 0.01f;
        private const float PortraitAspectThreshold = 1f;

        private readonly Camera _camera;
        private readonly IUpdater _updater;
        private readonly float _originalMinimumOrtoSize;
        private readonly float _originalMaximumOrtoSize;

        private float _effectiveMinimumOrtoSize;
        private float _effectiveMaximumOrtoSize;
        private float _lastAspect;
        private bool _isPortrait;

        public MapSceneCameraOrientationAdapter(Camera camera, IUpdater updater, ICameraConfig config)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _originalMinimumOrtoSize = config.MinimumOrtoSize;
            _originalMaximumOrtoSize = config.MaximumOrtoSize;

            if (_originalMinimumOrtoSize < 0)
                throw new ArgumentOutOfRangeException(nameof(_originalMinimumOrtoSize), _originalMinimumOrtoSize, "Value must be positive");

            if (_originalMaximumOrtoSize < 0)
                throw new ArgumentOutOfRangeException(nameof(_originalMaximumOrtoSize), _originalMaximumOrtoSize, "Value must be positive");

            if (_originalMaximumOrtoSize <= _originalMinimumOrtoSize)
            {
                throw new ArgumentException(
                    $"MaximumOrtoSize ({_originalMaximumOrtoSize}) must be greater than MinimumOrtoSize ({_originalMinimumOrtoSize})",
                    nameof(config));
            }

            _lastAspect = GetAspect();
            _isPortrait = _lastAspect < PortraitAspectThreshold;
            RecalculateEffectiveZoomBounds();
            AdjustCameraSizeToCurrentValue01();

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public event Action Changed;

        public float CurrentOrtoSize => _camera.orthographicSize;

        public float MinimumOrtoSize => _effectiveMinimumOrtoSize;

        public float MaximumOrtoSize => _effectiveMaximumOrtoSize;

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.Update);

        private void OnUpdate()
        {
            float currentAspect = GetAspect();

            if (Mathf.Abs(currentAspect - _lastAspect) < AspectComparisonEpsilon)
                return;

            _lastAspect = currentAspect;
            bool wasPortrait = _isPortrait;
            _isPortrait = currentAspect < PortraitAspectThreshold;

            if (_isPortrait == wasPortrait)
                return;

            float currentValue01 = ComputeValue01(_camera.orthographicSize, _effectiveMinimumOrtoSize, _effectiveMaximumOrtoSize);
            RecalculateEffectiveZoomBounds();
            float newSize = _effectiveMaximumOrtoSize - currentValue01 * (_effectiveMaximumOrtoSize - _effectiveMinimumOrtoSize);
            _camera.orthographicSize = Mathf.Clamp(newSize, _effectiveMinimumOrtoSize, _effectiveMaximumOrtoSize);

            InvokeChanged();
        }

        private void RecalculateEffectiveZoomBounds()
        {
            if (_isPortrait)
            {
                float aspectRatio = (float)Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
                _effectiveMinimumOrtoSize = _originalMinimumOrtoSize * aspectRatio;
                _effectiveMaximumOrtoSize = _originalMaximumOrtoSize * aspectRatio;
            }
            else
            {
                _effectiveMinimumOrtoSize = _originalMinimumOrtoSize;
                _effectiveMaximumOrtoSize = _originalMaximumOrtoSize;
            }
        }

        private void AdjustCameraSizeToCurrentValue01()
        {
            float currentValue01 = ComputeValue01(_camera.orthographicSize, _originalMinimumOrtoSize, _originalMaximumOrtoSize);
            float newSize = _effectiveMaximumOrtoSize - currentValue01 * (_effectiveMaximumOrtoSize - _effectiveMinimumOrtoSize);
            _camera.orthographicSize = Mathf.Clamp(newSize, _effectiveMinimumOrtoSize, _effectiveMaximumOrtoSize);

            InvokeChanged();
        }

        private float ComputeValue01(float currentSize, float minimumBound, float maximumBound)
        {
            float range = maximumBound - minimumBound;

            if (range <= 0f)
                throw new ArgumentOutOfRangeException(nameof(range), range, "Value must be positive");

            float normalized = (currentSize - minimumBound) / range;

            return 1f - normalized;
        }

        private float GetAspect() =>
            (float)Screen.width / Screen.height;

        private void InvokeChanged() =>
            Changed?.Invoke();
    }
}