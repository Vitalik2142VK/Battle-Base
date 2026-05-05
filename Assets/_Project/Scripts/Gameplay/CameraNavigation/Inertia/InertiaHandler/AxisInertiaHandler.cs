using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class AxisInertiaHandler
    {
        private readonly ICameraInertiaConfig _config;
        private readonly Queue<float> _velocityBuffer;

        private float _currentVelocity;

        public AxisInertiaHandler(ICameraInertiaConfig inertiaConfig)
        {
            _config = inertiaConfig ?? throw new ArgumentNullException(nameof(inertiaConfig));
            _velocityBuffer = new(_config.InertiaSmoothingWindow);
        }

        public void AddDelta(float worldDelta, float deltaTime)
        {
            if (deltaTime <= 0)
                return;

            float velocity = worldDelta / deltaTime;
            _velocityBuffer.Enqueue(velocity);

            while (_velocityBuffer.Count > _config.InertiaSmoothingWindow)
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

            if (Mathf.Abs(_currentVelocity) < _config.InertiaVelocityEpsilon)
                return false;

            _currentVelocity *= Mathf.Exp(-_config.InertiaDamping * deltaTime);

            if (Mathf.Abs(_currentVelocity) < _config.InertiaVelocityEpsilon)

                return false;

            velocity = _currentVelocity;

            return true;
        }

        public void DampenVelocity(float deltaTime, float factor)
        {
            if (deltaTime <= 0f)
                return;

            if (factor == 0)
            {
                _currentVelocity = 0;

                return;
            }

            float dynamicDamping = _config.InertiaExtraDampingFactor / factor;
            _currentVelocity = Mathf.MoveTowards(_currentVelocity, 0f, dynamicDamping * deltaTime);
        }

        public void ResetInertia() =>
            _currentVelocity = 0;
    }
}