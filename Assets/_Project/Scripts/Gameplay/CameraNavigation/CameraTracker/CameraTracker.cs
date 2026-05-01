using System;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public sealed class CameraTracker : ICameraTracker, IDisposable
    {
        private const float PositionSqrThreshold = 0.0001f;
        private const float RotationAngleThreshold = 0.01f;
        private const float OrthoSizeThreshold = 0.0001f;

        private static readonly UpdateType s_UpdateType = UpdateType.Update;

        private readonly Camera _camera;
        private readonly Transform _cameraTransform;
        private readonly IUpdater _updater;

        public CameraTracker(Camera camera, IUpdater updater)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _cameraTransform = camera.transform;

            CachedPosition = _cameraTransform.position;
            CachedRotation = _cameraTransform.rotation;
            CachedOrthoSize = _camera.orthographicSize;

            _updater.Subscribe(OnUpdate, s_UpdateType);
        }

        public event Action RotationChanged;
        public event Action PositionChanged;
        public event Action OrthoSizeChanged;

        public Vector3 CachedPosition { get; private set; }

        public Quaternion CachedRotation { get; private set; }

        public float CachedOrthoSize { get; private set; }

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, s_UpdateType);

        private void OnUpdate()
        {
            Vector3 currentPosition = _cameraTransform.position;

            if ((currentPosition - CachedPosition).sqrMagnitude > PositionSqrThreshold)
            {
                CachedPosition = currentPosition;
                PositionChanged?.Invoke();
            }

            Quaternion currentRotation = _cameraTransform.rotation;
            float angle = Quaternion.Angle(currentRotation, CachedRotation);

            if (angle > RotationAngleThreshold)
            {
                CachedRotation = currentRotation;
                RotationChanged?.Invoke();
            }

            if (Math.Abs(_camera.orthographicSize - CachedOrthoSize) > OrthoSizeThreshold)
            {
                CachedOrthoSize = _camera.orthographicSize;
                OrthoSizeChanged?.Invoke();
            }
        }
    }
}