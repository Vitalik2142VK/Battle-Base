using System;
using BattleBase.UpdateService;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseCameraInputReader : ICameraInputReader, IDisposable
    {
        private readonly IUpdater _updater;
        private readonly IUIPointerChecker _uiPointerChecker;
        private readonly MouseDragHandler _mouseDragHandler;
        private readonly KeyboardDragHandler _keyboardDragHandler;
        private readonly MouseZoomHandler _mouseZoomHandler;

        private bool _dragBlockedByUI;
        private bool _disposed;

        public MouseCameraInputReader(
            IUpdater updater,
            IUIPointerChecker uiPointerChecker,
            MouseDragHandler mouseDragHandler,
            KeyboardDragHandler keyboardDragHandler,
            MouseZoomHandler mouseZoomHandler)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _uiPointerChecker = uiPointerChecker ?? throw new ArgumentNullException(nameof(uiPointerChecker));
            _mouseDragHandler = mouseDragHandler ?? throw new ArgumentNullException(nameof(mouseDragHandler));
            _keyboardDragHandler = keyboardDragHandler ?? throw new ArgumentNullException(nameof(keyboardDragHandler));
            _mouseZoomHandler = mouseZoomHandler ?? throw new ArgumentNullException(nameof(mouseZoomHandler));

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public Vector3? WorldDragDelta { get; private set; }

        public float? ZoomDelta { get; private set; }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _updater?.Unsubscribe(OnUpdate, UpdateType.Update);
        }

        private void OnUpdate()
        {
            ReadDrag();
            ReadZoom();
        }

        private void ReadDrag()
        {
            if (Input.GetMouseButton(InputConstants.DragMouseButton))
                ReadMouseDrag();
            else
                ReadKeyboardDrag();
        }

        private void ReadMouseDrag()
        {
            if (_dragBlockedByUI && Input.GetMouseButton(InputConstants.DragMouseButton) == false)
            {
                _dragBlockedByUI = false;
                _mouseDragHandler.Reset();
            }

            bool buttonDown = Input.GetMouseButtonDown(InputConstants.DragMouseButton);
            bool buttonUp = Input.GetMouseButtonUp(InputConstants.DragMouseButton);

            if (_dragBlockedByUI && Input.GetMouseButton(InputConstants.DragMouseButton)
                && _uiPointerChecker.IsPointerOverUI(Input.mousePosition) == false)
            {
                _dragBlockedByUI = false;
                _mouseDragHandler.Reset();
            }

            if (buttonDown && _uiPointerChecker.IsPointerOverUI(Input.mousePosition))
            {
                _dragBlockedByUI = true;
                _mouseDragHandler.Reset();
                WorldDragDelta = null;

                return;
            }

            if (_dragBlockedByUI)
            {
                if (buttonUp)
                {
                    _dragBlockedByUI = false;
                    _mouseDragHandler.Reset();
                }

                WorldDragDelta = null;

                return;
            }

            Vector3? delta = _mouseDragHandler.Update(Input.mousePosition, buttonDown, buttonUp, true);
            WorldDragDelta = delta;
        }

        private void ReadKeyboardDrag() =>
            WorldDragDelta = _keyboardDragHandler.Update(Time.deltaTime);

        private void ReadZoom()
        {
            if (_uiPointerChecker.IsPointerOverUI(Input.mousePosition))
            {
                ZoomDelta = null;

                return;
            }

            ZoomDelta = _mouseZoomHandler.Update();
        }
    }
}