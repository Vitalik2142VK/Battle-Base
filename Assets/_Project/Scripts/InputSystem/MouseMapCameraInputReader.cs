using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.InputSystem
{
    public class MouseMapCameraInputReader : IMapCameraInputReader, IDisposable
    {
        private const string MouseX = "Mouse X";
        private const string MouseY = "Mouse Y";
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

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.FixedUpdate);

        private void OnUpdate() =>
            ReadInput();

        private void ReadInput()
        {
            if (Input.GetMouseButton(DragMouseButton))
            {
                DragDelta = ReadMouseDragDelta();

                return;
            }

            Vector2 keyboardDelta = ReadKeyboardDelta();

            if (keyboardDelta != Vector2.zero)
                DragDelta = keyboardDelta;
            else
                DragDelta = null;
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