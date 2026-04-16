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

        public void Update(float? zoomDelta)
        {
            if (zoomDelta.HasValue == false)
                return;

            float newSize = _camera.orthographicSize - zoomDelta.Value * _zoomSpeed;
            _camera.orthographicSize = Mathf.Clamp(newSize, _minZoom, _maxZoom);
        }
    }
}