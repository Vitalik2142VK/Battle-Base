using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraZoom : ICameraZoom
    {
        private readonly Camera _camera;
        private readonly ICameraOrientationAdapter _orientationAdapter;
        private readonly float _zoomSpeed;

        public CameraZoom(Camera camera, ICameraOrientationAdapter orientationAdapter, ICameraConfig config)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _orientationAdapter = orientationAdapter ?? throw new ArgumentNullException(nameof(orientationAdapter));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _zoomSpeed = config.ZoomSpeed;

            if (_zoomSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(_zoomSpeed), _zoomSpeed, "Value must be positive");
        }

        public event Action Changed;

        public float Value01
        {
            get
            {
                float range = MaximumZoom - MinimumZoom;

                if (range <= 0f)
                    throw new ArgumentOutOfRangeException(nameof(range), range, "Value must be positive");

                float normalized = (_camera.orthographicSize - MinimumZoom) / range;

                return 1f - normalized;
            }
        }

        private float MinimumZoom => _orientationAdapter.MinimumOrtoSize;

        private float MaximumZoom => _orientationAdapter.MaximumOrtoSize;

        public void SetValue01(float value)
        {
            float maximumZoom = MaximumZoom;
            float minimumZoom = MinimumZoom;

            float clampedValue = Mathf.Clamp01(value);
            float range = maximumZoom - minimumZoom;
            float targetSize = maximumZoom - clampedValue * range;
            SetCameraSize(targetSize);
        }

        public void Update(float? zoomDelta)
        {
            if (zoomDelta.HasValue == false)
                return;

            float newSize = _camera.orthographicSize - zoomDelta.Value * _zoomSpeed;
            SetCameraSize(newSize);
        }

        private void SetCameraSize(float size)
        {
            _camera.orthographicSize = Mathf.Clamp(size, MinimumZoom, MaximumZoom);
            Changed?.Invoke();
        }
    }
}