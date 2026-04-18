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

        public FrustumProjectionService(Camera camera, ICameraArea area)
        {
            _camera = camera ?? throw new System.ArgumentNullException(nameof(camera));
            _area = area ?? throw new System.ArgumentNullException(nameof(area));
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
    }
}