using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class TouchMapCameraInputReader : IMapCameraInputReader, IDisposable
    {
        private readonly IUpdater _updater;
        private readonly ITouchMapCameraInputReaderConfig _config;

        private Vector2 _previousDragPosition;
        private bool _isDragging;
        private bool _isPinching;
        private float _previousPinchDistance;

        public TouchMapCameraInputReader(IUpdater updater, ITouchMapCameraInputReaderConfig config)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public Vector2? DragDelta { get; private set; }

        public float? ZoomDelta { get; private set; }

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.Update);

        private void OnUpdate() =>
            ReadInput();

        private void ReadInput()
        {
            int touchCount = Input.touchCount;

            if (touchCount == 0)
            {
                _isDragging = false;
                _isPinching = false;
                DragDelta = null;
                ZoomDelta = null;

                return;
            }

            if (touchCount == 1 && _isPinching == false)
            {
                ProcessDrag();
            }
            else if (touchCount >= 2)
            {
                ProcessPinch();
            }
            else
            {
                _isDragging = false;
                _isPinching = false;
                DragDelta = null;
                ZoomDelta = null;
            }
        }

        private void ProcessDrag()
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _isDragging = true;
                _previousDragPosition = touch.position;
                DragDelta = null;

                return;
            }

            if (_isDragging == false)
                return;

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                Vector2 currentPos = touch.position;
                Vector2 delta = currentPos - _previousDragPosition;

                if (delta.magnitude > _config.DeadZone)
                    DragDelta = delta * _config.DragSensitivity;
                else
                    DragDelta = null;

                _previousDragPosition = currentPos;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isDragging = false;
                DragDelta = null;
            }
        }

        private void ProcessPinch()
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 prevPos0 = touch0.position - touch0.deltaPosition;
            Vector2 prevPos1 = touch1.position - touch1.deltaPosition;
            float prevDistance = Vector2.Distance(prevPos0, prevPos1);
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (_isPinching == false)
            {
                if (currentDistance >= _config.MinPinchDistance)
                {
                    _isPinching = true;
                    _previousPinchDistance = currentDistance;
                }

                DragDelta = null;
                ZoomDelta = null;

                return;
            }

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float delta = currentDistance - _previousPinchDistance;
                ZoomDelta = delta * _config.ZoomSensitivity;
                _previousPinchDistance = currentDistance;
            }
            else if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended ||
                     touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled)
            {
                _isPinching = false;
                ZoomDelta = null;
            }

            DragDelta = null;
        }
    }
}