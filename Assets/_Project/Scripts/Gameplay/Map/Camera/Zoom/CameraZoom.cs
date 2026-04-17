using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraZoom : ICameraZoom
    {
        private readonly Camera _camera;
        private readonly float _zoomSpeed;
        private readonly float _minZoom;
        private readonly float _maxZoom;

        public CameraZoom(Camera camera, ICameraConfig config)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _zoomSpeed = config.ZoomSpeed;
            _minZoom = config.MinimumZoom;
            _maxZoom = config.MaximumZoom;
        }

        public float Value01
        {
            get
            {
                float range = _maxZoom - _minZoom;

                if (range <= 0f) 
                    return 0.5f;

                float normalized = (_camera.orthographicSize - _minZoom) / range;

                return 1f - normalized;
            }
        }

        public void SetValue01(float value)
        {
            float clampedValue = Mathf.Clamp01(value);
            float range = _maxZoom - _minZoom;
            float targetSize = _maxZoom - clampedValue * range;
            _camera.orthographicSize = targetSize;
        }

        public void Update(float? zoomDelta)
        {
            if (zoomDelta.HasValue == false)
                return;

            float newSize = _camera.orthographicSize - zoomDelta.Value * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
        }
    }
}