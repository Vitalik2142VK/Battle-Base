using System;
using BattleBase.Utils;
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
            {
                Vector3 delta = worldDragDelta.Value;

                if (VectorValidation.IsValid(delta) == false)
                    return;

                ApplyMovement(delta);
            }
            else
            {
                _snapBack.Restore(_cameraTransform, deltaTime);
            }
        }

        private void ApplyMovement(Vector3 worldDelta)
        {
            Vector3 desiredPosition = _cameraTransform.position - worldDelta;
            Vector3 correctedDelta = ApplyResistance(worldDelta, desiredPosition);
            Vector3 finalDesiredPosition = _cameraTransform.position - correctedDelta;
            Vector3 restrictedPosition = RestrictToBounds(finalDesiredPosition);
            _cameraTransform.position = restrictedPosition;
        }

        private Vector3 ApplyResistance(Vector3 delta, Vector3 desiredPosition)
        {
            float overshootX = _boundsLimiter.GetOvershootX(desiredPosition);
            float overshootZ = _boundsLimiter.GetOvershootZ(desiredPosition);
            float maxOvershoot = _projector.Area.Overshoot;
            float resistance = _projector.Area.Resistance;

            Vector3 result = delta;

            if (overshootX > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootX / maxOvershoot) * resistance;
                result.x *= factor;
            }

            if (overshootZ > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootZ / maxOvershoot) * resistance;
                result.z *= factor;
            }

            return result;
        }

        private Vector3 RestrictToBounds(Vector3 desiredPosition)
        {
            Vector3 result = _cameraTransform.position;

            if (_boundsLimiter.IsValidPositionX(desiredPosition))
                result.x = desiredPosition.x;

            if (_boundsLimiter.IsValidPositionZ(desiredPosition))
                result.z = desiredPosition.z;

            return result;
        }
    }
}