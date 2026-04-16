using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraDragger : ICameraDragger
    {
        private readonly Camera _camera;
        private readonly ICameraBoundsLimiter _boundsLimiter;
        private readonly ICameraSnapBack _snapBack;
        private readonly ICameraFrustumProjector _projector;
        private readonly Transform _cameraTransform;

        public CameraDragger(
            Camera camera,
            ICameraFrustumProjector projector,
            ICameraSnapBack snapBack,
            ICameraBoundsLimiter boundsLimiter)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _projector = projector ?? throw new ArgumentNullException(nameof(projector));
            _boundsLimiter = boundsLimiter ?? throw new ArgumentNullException(nameof(boundsLimiter));
            _snapBack = snapBack ?? throw new ArgumentNullException(nameof(snapBack));
            _cameraTransform = _camera.transform;
        }

        public void Update(float deltaTime, Vector3? worldDragDelta)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Value must be positive");

            if (worldDragDelta.HasValue)
                ApplyMovement(worldDragDelta.Value);
            else
                _snapBack.Restore(_cameraTransform, deltaTime);
        }

        private void ApplyMovement(Vector3 worldDelta)
        {
            Vector3 newPos = _cameraTransform.position - worldDelta;
            float overshootX = _boundsLimiter.GetOvershootX(newPos);
            float overshootZ = _boundsLimiter.GetOvershootZ(newPos);
            float maxOvershoot = _projector.Area.Overshoot;
            float resistance = _projector.Area.Resistance;

            if (overshootX > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootX / maxOvershoot) * resistance;
                worldDelta.x *= factor;
            }

            if (overshootZ > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootZ / maxOvershoot) * resistance;
                worldDelta.z *= factor;
            }

            newPos = _cameraTransform.position - worldDelta;
            Vector3 finalPos = _cameraTransform.position;

            if (_boundsLimiter.IsValidPositionX(newPos))
                finalPos.x = newPos.x;
            else
                finalPos.x = _cameraTransform.position.x;

            if (_boundsLimiter.IsValidPositionZ(newPos))
                finalPos.z = newPos.z;
            else
                finalPos.z = _cameraTransform.position.z;

            _cameraTransform.position = finalPos;
        }
    }
}