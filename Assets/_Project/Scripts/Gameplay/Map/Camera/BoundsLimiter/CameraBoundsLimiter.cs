using System;
using System.Collections.Generic;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraBoundsLimiter : ICameraBoundsLimiter
    {
        private readonly ICameraFrustumProjector _projector;

        public CameraBoundsLimiter(ICameraFrustumProjector projector)
        {
            _projector = projector ?? throw new ArgumentNullException(nameof(projector));
        }

        public bool IsValidPositionX(Vector3 position) =>
            IsValidPositionAlongAxis(position, c => c.x, bounds => bounds.min.x, bounds => bounds.max.x);

        public bool IsValidPositionZ(Vector3 position) =>
            IsValidPositionAlongAxis(position, c => c.z, bounds => bounds.min.z, bounds => bounds.max.z);

        public float GetOvershootX(Vector3 position) =>
            GetOvershootAlongAxis(position, c => c.x, b => b.min.x, b => b.max.x);

        public float GetOvershootZ(Vector3 position) =>
            GetOvershootAlongAxis(position, c => c.z, b => b.min.z, b => b.max.z);

        private bool IsValidPositionAlongAxis(
            Vector3 position,
            Func<Vector3, float> getCornerCoord,
            Func<Bounds, float> getMinBound,
            Func<Bounds, float> getMaxBound)
        {
            if (VectorValidation.IsValid(position) == false)
                return false;

            List<Vector3> corners = _projector.ProjectCornersOntoPlaneFromPosition(position);
            Bounds bounds = _projector.Area.OvershootBounds;
            float minBound = getMinBound(bounds);
            float maxBound = getMaxBound(bounds);

            foreach (Vector3 corner in corners)
            {
                float coord = getCornerCoord(corner);

                if (coord < minBound || coord > maxBound)
                    return false;
            }

            return true;
        }

        private float GetOvershootAlongAxis(
            Vector3 position,
            Func<Vector3, float> getCoord,
            Func<Bounds, float> getMin,
            Func<Bounds, float> getMax)
        {
            if (VectorValidation.IsValid(position) == false)
                return 0f;

            List<Vector3> corners = _projector.ProjectCornersOntoPlaneFromPosition(position);
            Bounds bounds = _projector.Area.ColliderBounds;
            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (Vector3 corner in corners)
            {
                float val = getCoord(corner);

                if (val < min)
                    min = val;

                if (val > max)
                    max = val;
            }

            float boundMin = getMin(bounds);
            float boundMax = getMax(bounds);
            float overshoot = 0f;

            if (min < boundMin)
                overshoot = boundMin - min;

            if (max > boundMax)
                overshoot = Mathf.Max(overshoot, max - boundMax);

            return overshoot;
        }
    }
}