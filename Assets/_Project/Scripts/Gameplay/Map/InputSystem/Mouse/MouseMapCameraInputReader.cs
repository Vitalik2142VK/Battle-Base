using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseMapCameraInputReader : IMapCameraInputReader, IDisposable
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
        private const string MouseScrollWheel = "Mouse ScrollWheel";
        private const int DragMouseButton = 0;

        private const string KeyboardAxisX = "Horizontal";
        private const string KeyboardAxisY = "Vertical";

        private readonly IUpdater _updater;
        private readonly IMouseMapCameraInputReaderConfig _config;

        public MouseMapCameraInputReader(IUpdater updater, IMouseMapCameraInputReaderConfig config)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public Vector2? DragDelta { get; private set; }

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
            {
                DragDelta = ReadMouseDragDelta();
            }
            else
            {
                Vector2 keyboardDelta = ReadKeyboardDelta();
                DragDelta = keyboardDelta != Vector2.zero ? keyboardDelta : null;
            }
        }

        private void ReadZoom()
        {
            float scroll = Input.GetAxis(MouseScrollWheel);

            if (Mathf.Abs(scroll) > 0.001f)
                ZoomDelta = scroll * _config.ZoomSensitivity;
            else
                ZoomDelta = null;
        }

        private Vector2 ReadMouseDragDelta()
        {
            float deltaX = Input.GetAxis(MouseX) * _config.MouseSensitivity;
            float deltaY = Input.GetAxis(MouseY) * _config.MouseSensitivity;

            return new Vector2(deltaX, deltaY);
        }

        private Vector2 ReadKeyboardDelta()
        {
            float deltaX = Input.GetAxis(KeyboardAxisX) * _config.KeyboardSensitivity;
            float deltaY = Input.GetAxis(KeyboardAxisY) * _config.KeyboardSensitivity;

            return new Vector2(deltaX, deltaY);
        }
    }
}