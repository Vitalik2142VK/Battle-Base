using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class AxisInertiaHandler
    {
        private const float VelocityEpsilon = 0.01f;
        private const int SmoothingWindow = 4;

        private readonly float _damping;
        private readonly Queue<float> _velocityBuffer = new(SmoothingWindow);
        private float _currentVelocity;

        public AxisInertiaHandler(float damping)
        {
            _damping = damping;
        }

        public bool HasInertia => Mathf.Abs(_currentVelocity) > VelocityEpsilon;

        public void AddDelta(float worldDelta, float deltaTime)
        {
            if (deltaTime <= 0)
                return;

            float velocity = worldDelta / deltaTime;
            _velocityBuffer.Enqueue(velocity);

            while (_velocityBuffer.Count > SmoothingWindow)
                _velocityBuffer.Dequeue();

            float sum = 0f;

            foreach (float v in _velocityBuffer)
                sum += v;

            _currentVelocity = sum / _velocityBuffer.Count;
        }

        public bool TryGetVelocity(float deltaTime, out float velocity)
        {
            velocity = 0;

            if (deltaTime <= 0)
                return false;

            if (Mathf.Abs(_currentVelocity) < VelocityEpsilon)
                return false;

            _currentVelocity *= Mathf.Exp(-_damping * deltaTime);

            if (Mathf.Abs(_currentVelocity) < VelocityEpsilon)

                return false;

            velocity = _currentVelocity;

            return true;
        }

        public void DampenVelocity(float extraDampingPerSecond, float deltaTime)
        {
            if (deltaTime <= 0f)
                return;

            _currentVelocity = Mathf.MoveTowards(_currentVelocity, 0f, extraDampingPerSecond * deltaTime);
        }
    }
}