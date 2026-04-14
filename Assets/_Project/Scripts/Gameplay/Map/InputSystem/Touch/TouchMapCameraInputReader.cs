using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class TouchMapCameraInputReader : IMapCameraInputReader, IDisposable
    {
        private const float PixelDeltaThreshold = 0.01f;

        private readonly IUpdater _updater;
        private readonly ITouchMapCameraInputReaderConfig _config;
        private readonly Camera _camera;

        private readonly TouchDragHandler _dragHandler;
        private readonly TouchPinchHandler _pinchHandler;

        public TouchMapCameraInputReader(IUpdater updater, ITouchMapCameraInputReaderConfig config)
        {
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _camera = Camera.main;

            _dragHandler = new TouchDragHandler(_camera, PixelDeltaThreshold);
            _pinchHandler = new TouchPinchHandler(_config);

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
            WorldDragDelta = null;
            ZoomDelta = null;
        }

        private void ReadDrag()
        {
            WorldDragDelta = _dragHandler.Update(Input.GetTouch(0));
            ZoomDelta = null;
        }

        private void ReadZoom()
        {
            ZoomDelta = _pinchHandler.Update(Input.GetTouch(0), Input.GetTouch(1));
            WorldDragDelta = null;
        }
    }
}