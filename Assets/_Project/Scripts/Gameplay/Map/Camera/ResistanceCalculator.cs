using System;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class ResistanceCalculator
    {
        private readonly ICameraBoundsLimiter _boundsLimiter;
        private readonly ICameraArea _cameraArea;

        public ResistanceCalculator(ICameraBoundsLimiter boundsLimiter, ICameraArea cameraArea)
        {
            _boundsLimiter = boundsLimiter ?? throw new ArgumentNullException(nameof(boundsLimiter));
            _cameraArea = cameraArea ?? throw new ArgumentNullException(nameof(cameraArea));
        }

        public Vector3 Calculate(Vector3 delta, Vector3 desiredPosition)
        {
            if (VectorValidation.IsValid(delta) == false)
                throw new ArgumentException($"Delta is invalid (NaN or Infinity): {delta}", nameof(delta));
            
            if (VectorValidation.IsValid(desiredPosition) == false)
                throw new ArgumentException($"Desired position is invalid (NaN or Infinity): {desiredPosition}", nameof(desiredPosition));

            float overshootX = _boundsLimiter.GetOvershootX(desiredPosition);
            float overshootZ = _boundsLimiter.GetOvershootZ(desiredPosition);
            float maxOvershoot = _cameraArea.ResistanceFadeDistance;
            float resistance = _cameraArea.Resistance;

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
    }
}