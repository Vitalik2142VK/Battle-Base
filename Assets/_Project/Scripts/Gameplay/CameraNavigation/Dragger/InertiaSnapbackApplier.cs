using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class InertiaSnapbackApplier
    {
        private const float ExtraDampingFactor = 3000f;

        private readonly Transform _cameraRig;
        private readonly ICameraSnapBack _snapBack;
        private readonly ICameraBoundsLimiter _boundsLimiter;
        private readonly PositionRestrictor _restrictor;
        private readonly float _restoreSpeed;

        private readonly AxisInertiaHandler _inertiaRight;
        private readonly AxisInertiaHandler _inertiaForward;

        public InertiaSnapbackApplier(
            Transform cameraRig,
            ICameraSnapBack snapBack,
            ICameraBoundsLimiter boundsLimiter,
            PositionRestrictor restrictor,
            ICameraConfig config)
        {
            _cameraRig = cameraRig;
            _snapBack = snapBack;
            _boundsLimiter = boundsLimiter;
            _restrictor = restrictor;

            _restoreSpeed = config.RestoreSpeed;
            float inertiaDamping = config.InertiaDamping;

            _inertiaRight = new AxisInertiaHandler(inertiaDamping);
            _inertiaForward = new AxisInertiaHandler(inertiaDamping);
        }

        public void UpdateInertia(Vector3 worldDragDelta, float deltaTime)
        {
            _inertiaRight.AddDelta(worldDragDelta.x, deltaTime);
            _inertiaForward.AddDelta(worldDragDelta.z, deltaTime);
        }

        public void Apply(float deltaTime)
        {
            float rightShift = ComputeRightAxis(deltaTime);
            float forwardShift = ComputeForwardAxis(deltaTime);

            Vector3 moveDelta = _cameraRig.right * rightShift + _cameraRig.forward * forwardShift;
            Vector3 newPosition = _cameraRig.position - moveDelta;
            newPosition = _restrictor.Restrict(newPosition, _cameraRig.position);
            _cameraRig.position = newPosition;
        }

        private float ComputeRightAxis(float deltaTime) =>
            ComputeAxisShift(_inertiaRight, deltaTime, _cameraRig.right, isXAxis: true);

        private float ComputeForwardAxis(float deltaTime) =>
            ComputeAxisShift(_inertiaForward, deltaTime, _cameraRig.forward, isXAxis: false);

        private float ComputeAxisShift(
            AxisInertiaHandler inertia,
            float deltaTime,
            Vector3 axisDirection,
            bool isXAxis)
        {
            if (inertia.TryGetVelocity(deltaTime, out float velocity))
            {
                float deltaMove = velocity * deltaTime;
                Vector3 desiredPosition = _cameraRig.position + axisDirection * deltaMove;

                float overshoot = isXAxis
                    ? _boundsLimiter.GetOvershootX(desiredPosition)
                    : _boundsLimiter.GetOvershootZ(desiredPosition);

                if (overshoot > 0)
                {
                    inertia.DampenVelocity(ExtraDampingFactor, deltaTime);

                    if (inertia.TryGetVelocity(deltaTime, out velocity))
                        deltaMove = velocity * deltaTime;
                    else
                        deltaMove = 0f;
                }

                return deltaMove;
            }

            Vector3 worldCorrection = _snapBack.GetCorrection(_cameraRig.position);
            float snapbackShift = -Vector3.Dot(worldCorrection, axisDirection);
            float maximumSnapback = _restoreSpeed * deltaTime;
            snapbackShift = Mathf.Clamp(snapbackShift, -maximumSnapback, maximumSnapback);

            return snapbackShift;
        }
    }
}