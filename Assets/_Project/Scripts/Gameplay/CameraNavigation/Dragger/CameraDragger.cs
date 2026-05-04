using System;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class CameraDragger : ICameraDragger
    {
        private readonly DragApplier _dragApplier;
        private readonly InertiaSnapbackApplier _inertiaApplier;

        public CameraDragger(
            CameraRig cameraRig,
            ICameraAreaService cameraAreaService,
            ICameraSnapBack snapBack,
            ICameraBoundsLimiter boundsLimiter,
            ICameraConfig config)
        {
            if (cameraRig == null)
                throw new ArgumentNullException(nameof(cameraRig));
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            Transform cameraRigTransform = cameraRig.transform;
            ResistanceCalculator resistanceCalculator = new(boundsLimiter, cameraAreaService);
            PositionRestrictor positionRestrictor = new(boundsLimiter);

            _dragApplier = new DragApplier(
                cameraRigTransform,
                resistanceCalculator,
                positionRestrictor);

            _inertiaApplier = new InertiaSnapbackApplier(
                cameraRigTransform,
                snapBack,
                boundsLimiter,
                positionRestrictor,
                config);
        }

        public void Update(float deltaTime, Vector3? worldDragDelta)
        {
            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime));

            if (worldDragDelta.HasValue)
            {
                _inertiaApplier.UpdateInertia(worldDragDelta.Value, deltaTime);
                _dragApplier.Apply(worldDragDelta.Value);
            }
            else
            {
                _inertiaApplier.Apply(deltaTime);
            }
        }
    }
}