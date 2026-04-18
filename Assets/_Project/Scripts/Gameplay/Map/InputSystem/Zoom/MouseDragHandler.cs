using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseDragHandler
    {
        private readonly float _pixelDeltaThreshold;

        private Camera _camera;
        private Vector2 _lastMousePosition;
        private bool _isDragging;

        public MouseDragHandler(Camera camera, float pixelDeltaThreshold)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

            if (pixelDeltaThreshold <= 0)
                throw new ArgumentOutOfRangeException(nameof(pixelDeltaThreshold), pixelDeltaThreshold, "Value must be positive");

            _pixelDeltaThreshold = pixelDeltaThreshold;
        }

        public Vector3? Update(Vector2 currentMousePos, bool buttonDown, bool buttonUp, bool buttonPressed)
        {
            if (_camera == null)
                _camera = Camera.main;

            if (buttonDown)
            {
                _isDragging = true;
                _lastMousePosition = currentMousePos;

                return null;
            }

            if (_isDragging == false)
                return null;

            if (buttonPressed)
            {
                Vector2 pixelDelta = currentMousePos - _lastMousePosition;
                _lastMousePosition = currentMousePos;

                if (pixelDelta.magnitude > _pixelDeltaThreshold)
                    return CameraDragHelper.PixelDeltaToWorldDelta(_camera, pixelDelta);
                else
                    return Vector3.zero;
            }
            else if (buttonUp)
            {
                _isDragging = false;

                return null;
            }

            return null;
        }

        public void Reset() =>
            _isDragging = false;
    }
}