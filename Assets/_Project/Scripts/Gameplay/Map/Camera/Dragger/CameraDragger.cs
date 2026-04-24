using System;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraDragger : ICameraDragger
    {
        private readonly Camera _camera;
        private readonly ICameraSnapBack _snapBack;
        private readonly Transform _cameraTransform;

        private readonly ResistanceCalculator _resistanceCalculator;
        private readonly PositionRestrictor _positionRestrictor;

        public CameraDragger(
            Camera camera,
            ICameraFrustumProjector frustumProjector,
            ICameraSnapBack snapBack,
            ICameraBoundsLimiter boundsLimiter)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _snapBack = snapBack ?? throw new ArgumentNullException(nameof(snapBack));
            _cameraTransform = _camera.transform;

            _resistanceCalculator = new ResistanceCalculator(boundsLimiter, frustumProjector.Area);
            _positionRestrictor = new PositionRestrictor(boundsLimiter);
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
            Vector3 correctedDelta = _resistanceCalculator.Calculate(worldDelta, desiredPosition);
            Vector3 finalDesiredPosition = _cameraTransform.position - correctedDelta;
            Vector3 restrictedPosition = _positionRestrictor.Restrict(finalDesiredPosition, _cameraTransform.position);
            _cameraTransform.position = restrictedPosition;
        }
    }
}