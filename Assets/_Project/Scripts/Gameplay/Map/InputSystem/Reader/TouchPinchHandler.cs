using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class TouchPinchHandler
    {
        private readonly ITouchMapCameraInputReaderConfig _config;
        private readonly float _minPinchDistance;

        private bool _isPinching;
        private float _previousPinchDistance;

        public TouchPinchHandler(ITouchMapCameraInputReaderConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _minPinchDistance = config.MinPinchDistance;
        }

        public float? Update(Touch touch0, Touch touch1)
        {
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (_isPinching == false)
            {
                if (currentDistance >= _minPinchDistance)
                {
                    _isPinching = true;
                    _previousPinchDistance = currentDistance;
                }

                return null;
            }

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float delta = currentDistance - _previousPinchDistance;
                _previousPinchDistance = currentDistance;

                return delta * _config.ZoomSensitivity;
            }
            else if (
                touch0.phase == TouchPhase.Ended
                || touch1.phase == TouchPhase.Ended
                || touch0.phase == TouchPhase.Canceled
                || touch1.phase == TouchPhase.Canceled)
            {
                _isPinching = false;

                return null;
            }

            return null;
        }

        public void Reset() =>
            _isPinching = false;
    }
}