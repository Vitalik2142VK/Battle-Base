using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraDragger
    {
        private readonly Transform _cameraTransform;
        private readonly CameraFrustumProjector _projector;
        private readonly float _dragSpeed;

        private readonly CameraBoundsLimiter _boundsLimiter;
        private readonly CameraSnapBack _snapBack;

        public CameraDragger(
            Transform cameraTransform,
            CameraFrustumProjector projector,
            float dragSpeed,
            float restoreSpeed)
        {
            _cameraTransform = cameraTransform != null ? cameraTransform : throw new ArgumentNullException(nameof(cameraTransform));
            _projector = projector != null ? projector : throw new ArgumentNullException(nameof(projector));

            if (dragSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(dragSpeed), dragSpeed, "Value must be positive");

            _dragSpeed = dragSpeed;

            if (restoreSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(restoreSpeed), restoreSpeed, "Value must be positive");

            _boundsLimiter = new CameraBoundsLimiter(projector);
            _snapBack = new CameraSnapBack(projector, restoreSpeed);
        }

        public void Update(float deltaTime, Vector2? dragDelta)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Value must be positive");

            if (dragDelta.HasValue)
                ProcessDrag(dragDelta.Value, deltaTime);
            else
                _snapBack.Restore(_cameraTransform, deltaTime);
        }

        private void ProcessDrag(Vector2 delta, float deltaTime)
        {
            Vector3 move = _dragSpeed * deltaTime * new Vector3(delta.x, 0, delta.y);
            Vector3 newPos = _cameraTransform.position - move;

            float overshootX = _boundsLimiter.GetOvershootX(newPos);
            float overshootZ = _boundsLimiter.GetOvershootZ(newPos);

            float maxOvershoot = _projector.Area.Overshoot;
            float resistance = _projector.Area.Resistance;

            if (overshootX > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootX / maxOvershoot) * resistance;
                move.x *= factor;
            }

            if (overshootZ > 0f)
            {
                float factor = 1f - Mathf.Clamp01(overshootZ / maxOvershoot) * resistance;
                move.z *= factor;
            }

            newPos = _cameraTransform.position - move;
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