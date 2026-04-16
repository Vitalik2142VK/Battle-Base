using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class TouchCameraInputReader : ICameraInputReader, IDisposable
    {
        private readonly IUpdater _updater;
        private readonly IUIPointerChecker _uiPointerChecker;
        private readonly TouchDragHandler _dragHandler;
        private readonly TouchPinchHandler _pinchHandler;

        private bool _dragBlockedByUI;
        private bool _pinchBlockedByUI;

        public TouchCameraInputReader(
            IUpdater updater,
            IUIPointerChecker uiPointerChecker,
            TouchDragHandler dragHandler,
            TouchPinchHandler pinchHandler)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _uiPointerChecker = uiPointerChecker ?? throw new ArgumentNullException(nameof(uiPointerChecker));
            _dragHandler = dragHandler ?? throw new ArgumentNullException(nameof(dragHandler));
            _pinchHandler = pinchHandler ?? throw new ArgumentNullException(nameof(pinchHandler));

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public Vector3? WorldDragDelta { get; private set; }

        public float? ZoomDelta { get; private set; }

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.Update);

        private void OnUpdate() =>
            ReadInput();

        private void ReadInput()
        {
            int touchCount = Input.touchCount;

            if (touchCount == 1 && ZoomDelta == null)
            {
                ReadDrag();

                return;
            }

            if (touchCount >= 2)
            {
                ReadZoom();

                return;
            }

            ResetAll();
        }

        private void ResetAll()
        {
            _dragHandler.Reset();
            _pinchHandler.Reset();

            _dragBlockedByUI = false;
            _pinchBlockedByUI = false;

            WorldDragDelta = null;
            ZoomDelta = null;
        }

        private void ReadDrag()
        {
            Touch touch = Input.GetTouch(0);

            if (_dragBlockedByUI && touch.phase == TouchPhase.Began
                && _uiPointerChecker.IsPointerOverUI(touch.position) == false)
            {
                _dragBlockedByUI = false;
                _dragHandler.Reset();
            }

            if (touch.phase == TouchPhase.Began
                && _uiPointerChecker.IsPointerOverUI(touch.position))
            {
                _dragBlockedByUI = true;
                _dragHandler.Reset();
                WorldDragDelta = null;

                return;
            }

            if (_dragBlockedByUI)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    _dragBlockedByUI = false;
                    _dragHandler.Reset();
                }

                WorldDragDelta = null;

                return;
            }

            WorldDragDelta = _dragHandler.Update(touch);
            ZoomDelta = null;
        }

        private void ReadZoom()
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (_pinchBlockedByUI &&
                touch0.phase == TouchPhase.Began && !_uiPointerChecker.IsPointerOverUI(touch0.position)
                && touch1.phase == TouchPhase.Began && !_uiPointerChecker.IsPointerOverUI(touch1.position))
            {
                _pinchBlockedByUI = false;
                _pinchHandler.Reset();
            }

            bool beganOnUI = (touch0.phase == TouchPhase.Began && _uiPointerChecker.IsPointerOverUI(touch0.position))
                || (touch1.phase == TouchPhase.Began && _uiPointerChecker.IsPointerOverUI(touch1.position));

            if (beganOnUI)
            {
                _pinchBlockedByUI = true;
                _pinchHandler.Reset();
                ZoomDelta = null;

                return;
            }

            if (_pinchBlockedByUI)
            {
                bool ended = (touch0.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled)
                    && (touch1.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Canceled);

                if (ended)
                {
                    _pinchBlockedByUI = false;
                    _pinchHandler.Reset();
                }

                ZoomDelta = null;

                return;
            }

            ZoomDelta = _pinchHandler.Update(touch0, touch1);
            WorldDragDelta = null;
        }
    }
}