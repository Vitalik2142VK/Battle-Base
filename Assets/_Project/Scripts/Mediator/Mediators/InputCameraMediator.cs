using System;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.Mediators
{
    public class InputCameraMediator : MediatorBase, IInjectable
    {
        [SerializeField] private bool _isDynamicAngle;

        private ICameraInputReader _inputReader;
        private ICameraDragger _dragger;
        private ICameraZoom _zoom;
        private ICameraAngleCompensator _angleCompensator;
        private float _dragVerticalFactor;

        [Inject]
        public void Construct(
            ICameraInputReader inputReader,
            ICameraDragger dragger,
            ICameraAngleCompensator angleCompensator,
            ICameraZoom zoom)
        {
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));
            _dragger = dragger ?? throw new ArgumentNullException(nameof(dragger));
            _angleCompensator = angleCompensator ?? throw new ArgumentNullException(nameof(angleCompensator));
            _zoom = zoom ?? throw new ArgumentNullException(nameof(zoom));
        }

        public override void Init() =>
            UpdateCompensation();

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            Vector3? dragDelta = _inputReader?.WorldDragDelta;

            if (dragDelta.HasValue)
            {
                Vector3 delta = dragDelta.Value;

                if (_isDynamicAngle)
                    UpdateCompensation();

                delta.z *= _dragVerticalFactor;
                dragDelta = delta;
            }

            _dragger.Update(deltaTime, dragDelta);
            _zoom.Update(_inputReader?.ZoomDelta);
        }

        private void UpdateCompensation() =>
            _dragVerticalFactor = _angleCompensator.CalculateVerticalFactor();
    }
}