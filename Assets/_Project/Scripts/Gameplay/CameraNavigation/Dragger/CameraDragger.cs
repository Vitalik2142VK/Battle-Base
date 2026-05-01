using System;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class CameraDragger : ICameraDragger
    {
        private readonly Camera _camera;
        private readonly Transform _cameraRig;
        private readonly ICameraSnapBack _snapBack;
        private readonly Transform _cameraTransform;

        private readonly ResistanceCalculator _resistanceCalculator;
        private readonly PositionRestrictor _positionRestrictor;

        public CameraDragger(
            Camera camera,
            CameraRig cameraRig,
            ICameraAreaService cameraAreaService,
            ICameraSnapBack snapBack,
            ICameraBoundsLimiter boundsLimiter)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

            if(cameraRig == null)
                throw new ArgumentNullException(nameof(cameraRig));

            _cameraRig = cameraRig.transform;

            _snapBack = snapBack ?? throw new ArgumentNullException(nameof(snapBack));
            _cameraTransform = _camera.transform;

            _resistanceCalculator = new ResistanceCalculator(boundsLimiter, cameraAreaService);
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
            Vector3 deltaGround = _cameraRig.right * worldDelta.x + _cameraRig.forward * worldDelta.z;
            deltaGround.y = 0f;

            Vector3 desiredRigPosition = _cameraRig.position - deltaGround;
            Vector3 correctedDelta = _resistanceCalculator.Calculate(deltaGround, desiredRigPosition);
            Vector3 finalDesiredRigPosition = _cameraRig.position - correctedDelta;
            Vector3 restrictedPosition = _positionRestrictor.Restrict(finalDesiredRigPosition, _cameraRig.position);
            _cameraRig.position = restrictedPosition;
        }
    }
}