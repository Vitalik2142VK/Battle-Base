using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace BattleBase.Gameplay.Map
{
    public class CameraSnapBack : ICameraSnapBack
    {
        private const float Half = 0.5f;

        private readonly ICameraFrustumProjector _frustumProjector;
        private readonly float _restoreSpeed;

        public CameraSnapBack(ICameraFrustumProjector frustumProjector, ICameraConfig config)
        {
            _frustumProjector = frustumProjector ?? throw new ArgumentNullException(nameof(frustumProjector));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _restoreSpeed = config.RestoreSpeed;
        }

        public void Restore(Transform cameraTransform, float deltaTime)
        {
            if (cameraTransform == null)
                throw new ArgumentNullException(nameof(cameraTransform));

            if (deltaTime < 0)
                throw new ArgumentOutOfRangeException(nameof(deltaTime), deltaTime, "Value must be positive");

            List<Vector3> corners = new();
            _frustumProjector.ProjectCornersOntoPlaneFromPosition(cameraTransform.position, corners);

            Bounds bounds = _frustumProjector.Area.ColliderBounds;
            Vector2 minMaxX = GetMinMax(corners, true);
            Vector2 minMaxZ = GetMinMax(corners, false);
            Vector3 correction = Vector3.zero;

            correction.x = CalculateCorrection(
                minMaxX.x,
                minMaxX.y,
                bounds.min.x,
                bounds.max.x,
                bounds.center.x);

            correction.z = CalculateCorrection(
                minMaxZ.x,
                minMaxZ.y,
                bounds.min.z,
                bounds.max.z,
                bounds.center.z);

            if (correction != Vector3.zero)
            {
                Vector3 targetPos = cameraTransform.position + correction;
                cameraTransform.position = Vector3.MoveTowards(cameraTransform.position, targetPos, _restoreSpeed * deltaTime);
            }
        }

        private Vector2 GetMinMax(List<Vector3> corners, bool isX)
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (Vector3 corner in corners)
            {
                float value = isX ? corner.x : corner.z;

                if (value < min)
                    min = value;

                if (value > max)
                    max = value;
            }

            return new Vector2(min, max);
        }

        private float CalculateCorrection(float min, float max, float boundMin, float boundMax, float boundCenter)
        {
            bool minInside = min >= boundMin;
            bool maxInside = max <= boundMax;

            if (minInside == false && maxInside == false)
            {
                float frustumCenter = (min + max) * Half;

                return boundCenter - frustumCenter;
            }

            if (minInside == false)
                return boundMin - min;

            if (maxInside == false)
                return boundMax - max;

            return 0f;
        }
    }
}