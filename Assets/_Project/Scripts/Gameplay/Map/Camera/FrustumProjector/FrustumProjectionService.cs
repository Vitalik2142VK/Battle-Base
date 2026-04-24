using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class FrustumProjectionService : IFrustumProjectionService
    {
        private const float NearClipOffset = 0.01f;

        private static readonly Vector3[] s_viewportCorners = new Vector3[]
        {
            new(0, 0, NearClipOffset),
            new(1, 0, NearClipOffset),
            new(1, 1, NearClipOffset),
            new(0, 1, NearClipOffset),
        };

        private readonly Camera _camera;
        private readonly ICameraArea _area;

        private Vector3 _cachedCameraPosition;
        private readonly List<Vector3> _cachedCorners = new ();
        private bool _isCacheValid;

        public FrustumProjectionService(Camera camera, ICameraArea area)
        {
            _camera = camera != null ? camera : throw new System.ArgumentNullException(nameof(camera));
            _area = area ?? throw new System.ArgumentNullException(nameof(area));
        }

        public IReadOnlyList<Vector3> Corners
        {
            get
            {
                Vector3 currentCameraPosition = _camera.transform.position;
                if (!_isCacheValid || currentCameraPosition != _cachedCameraPosition)
                {
                    RefreshCache(currentCameraPosition);
                }
                return _cachedCorners;
            }
        }

        public Vector3 ProjectedCenter
        {
            get
            {
                IReadOnlyList<Vector3> _ = Corners;

                if (_cachedCorners.Count == 0)
                    return Vector3.zero;

                Vector3 sum = Vector3.zero;

                foreach (var corner in _cachedCorners)
                    sum += corner;

                return sum / _cachedCorners.Count;
            }
        }

        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners)
        {
            outCorners.Clear();

            Transform originalTransform = _camera.transform;
            Vector3 originalPos = originalTransform.position;
            Quaternion originalRot = originalTransform.rotation;
            originalTransform.position = cameraPosition;
            Plane plane = new(Vector3.up, _area.GroundPlaneY);

            foreach (Vector3 viewportCorner in s_viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Vector3 projected = ProjectPointOntoPlane(worldCorner, plane);
                outCorners.Add(projected);
            }

            originalTransform.SetPositionAndRotation(originalPos, originalRot);
        }

        private Vector3 ProjectPointOntoPlane(Vector3 point, Plane plane)
        {
            Ray ray = new(point, _camera.transform.forward);

            return plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : point;
        }

        private void RefreshCache(Vector3 cameraPosition)
        {
            _cachedCorners.Clear();

            Transform originalTransform = _camera.transform;
            Vector3 originalPos = originalTransform.position;
            Quaternion originalRot = originalTransform.rotation;
            originalTransform.position = cameraPosition;
            Plane plane = new(Vector3.up, _area.GroundPlaneY);

            foreach (Vector3 viewportCorner in s_viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Vector3 projected = ProjectPointOntoPlane(worldCorner, plane);
                _cachedCorners.Add(projected);
            }

            originalTransform.SetPositionAndRotation(originalPos, originalRot);

            _cachedCameraPosition = cameraPosition;
            _isCacheValid = true;
        }
    }
}