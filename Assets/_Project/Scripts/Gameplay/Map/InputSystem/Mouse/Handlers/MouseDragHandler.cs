using System;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseDragHandler
    {
        private readonly Camera _camera;
        private readonly float _dragDeltaThreshold;

        private Vector2 _lastMousePosition;
        private bool _isDragging;

        public MouseDragHandler(Camera camera, IDragConfig config)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _dragDeltaThreshold = config.DragDeltaThreshold;
        }

        public Vector3? Update(Vector2 currentMousePos, bool buttonDown, bool buttonUp, bool buttonPressed)
        {
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

                if (pixelDelta.magnitude > _dragDeltaThreshold)
                    return CameraDragHelper.ConvertPixelDeltaToWorldDelta(_camera, pixelDelta);
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

        public void Reset()
        {
            _isDragging = false;
            _lastMousePosition = Vector2.zero;
        }
    }
}