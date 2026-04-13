using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraZoom
    {
        private readonly Camera _camera;
        private readonly float _zoomSpeed;
        private readonly float _minZoom;
        private readonly float _maxZoom;

        public CameraZoom(Camera camera, float zoomSpeed, float minZoom, float maxZoom)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

            if (zoomSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(zoomSpeed), zoomSpeed, "Value must be positive");

            _zoomSpeed = zoomSpeed;

            if (minZoom <= 0)
                throw new ArgumentOutOfRangeException(nameof(minZoom), minZoom, "Value must be positive");

            _minZoom = minZoom;

            if (maxZoom <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxZoom), maxZoom, "Value must be positive");

            _maxZoom = maxZoom;
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