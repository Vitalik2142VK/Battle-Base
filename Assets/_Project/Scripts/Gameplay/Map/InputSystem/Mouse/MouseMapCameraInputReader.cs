using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseMapCameraInputReader : IMapCameraInputReader, IDisposable
    {
        private const string MouseScrollWheel = "Mouse ScrollWheel";
        private const int DragMouseButton = 0;
        private const float PixelDeltaThreshold = 0.01f;
        private const float KeyboardAxisThreshold = 0.01f;
        private const float ScrollThreshold = 0.001f;

        private readonly IUpdater _updater;
        private readonly IMouseMapCameraInputReaderConfig _config;
        private readonly Camera _camera;

        private readonly MouseDragHandler _mouseDragHandler;
        private readonly KeyboardDragHandler _keyboardDragHandler;

        public MouseMapCameraInputReader(IUpdater updater, IMouseMapCameraInputReaderConfig config)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _camera = Camera.main;

            if(_camera == null)
                throw new ArgumentNullException(nameof(_camera));

            _mouseDragHandler = new MouseDragHandler(_camera, PixelDeltaThreshold);
            _keyboardDragHandler = new KeyboardDragHandler(_config.KeyboardSpeed, KeyboardAxisThreshold);

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public Vector3? WorldDragDelta { get; private set; }

        public float? ZoomDelta { get; private set; }

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.FixedUpdate);

        private void OnUpdate()
        {
            ReadDrag();
            ReadZoom();
        }

        private void ReadDrag()
        {
            if (Input.GetMouseButton(DragMouseButton))
                ReadMouseDrag();
            else
                ReadKeyboardDrag();
        }

        private void ReadMouseDrag()
        {
            bool buttonDown = Input.GetMouseButtonDown(DragMouseButton);
            bool buttonUp = Input.GetMouseButtonUp(DragMouseButton);
            Vector3? delta = _mouseDragHandler.Update(Input.mousePosition, buttonDown, buttonUp, true);
            WorldDragDelta = delta;
        }

        private void ReadKeyboardDrag() =>
            WorldDragDelta = _keyboardDragHandler.Update(Time.deltaTime);

        private void ReadZoom()
        {
            float scroll = Input.GetAxis(MouseScrollWheel);

            if (Mathf.Abs(scroll) > ScrollThreshold)
                ZoomDelta = scroll * _config.ZoomSensitivity;
            else
                ZoomDelta = null;
        }
    }
}