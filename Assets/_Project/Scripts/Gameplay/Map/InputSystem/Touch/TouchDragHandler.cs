using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class TouchDragHandler
    {
        private readonly Camera _camera;
        private readonly float _pixelDeltaThreshold;

        private Vector2 _lastDragPosition;
        private bool _isDragging;

        public TouchDragHandler(Camera camera, float pixelDeltaThreshold)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

            if (pixelDeltaThreshold <= 0)
                throw new ArgumentOutOfRangeException(
                    nameof(pixelDeltaThreshold),
                    pixelDeltaThreshold,
                    "Value mast be positive");

            _pixelDeltaThreshold = pixelDeltaThreshold;
        }

        public Vector3? Update(Touch touch)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _isDragging = true;
                _lastDragPosition = touch.position;
                return null;
            }

            if (!_isDragging)
                return null;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector2 pixelDelta = touch.position - _lastDragPosition;
                _lastDragPosition = touch.position;

                if (pixelDelta.magnitude > _pixelDeltaThreshold)
                {
                    Vector3 worldDelta = CameraDragHelper.PixelDeltaToWorldDelta(_camera, pixelDelta);

                    return worldDelta;
                }
                else
                {
                    return Vector3.zero;
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
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