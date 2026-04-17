using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(Camera))]
    public class CameraFrustumProjector : MonoBehaviour, ICameraFrustumProjector
    {
        private const float SphereRadius = 0.1f;
        private const float NearClipOffset = 0.01f;
        private const int CornerCount = 4;

        [SerializeField] private Camera _camera;
        [SerializeField] private CameraArea _area;
        [SerializeField] private Color _gizmoColor = Color.yellow;

        public ICameraArea Area => _area;

        private void OnDrawGizmos()
        {
            if (_camera == null)
                _camera = GetComponent<Camera>();

            if (_area == null)
                _area = FindObjectOfType<CameraArea>();

            Gizmos.color = _gizmoColor;
            List<Vector3> corners = ProjectCornersOntoPlaneFromPosition(_camera.transform.position);

            if (corners.Count == CornerCount)
            {
                foreach (Vector3 corner in corners)
                    Gizmos.DrawWireSphere(corner, SphereRadius);

                for (int i = 0; i < corners.Count; i++)
                    Gizmos.DrawLine(corners[i], corners[(i + 1) % corners.Count]);
            }
        }

        public List<Vector3> ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition)
        {
            if (_camera == null)
                throw new NullReferenceException(nameof(_camera));

            if (_area == null)
                throw new NullReferenceException(nameof(_area));

            _camera.transform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);
            _camera.transform.position = cameraPosition;

            List<Vector3> corners = new();
            Plane plane = new(Vector3.up, new Vector3(0, _area.PlaneY, 0));
            float nearClipPlane = _camera.nearClipPlane + NearClipOffset;
            Vector3[] viewportCorners = GetViewportCorners(nearClipPlane);

            foreach (Vector3 viewportCorner in viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Vector3 projectedPoint = ProjectPointOntoPlane(worldCorner, plane);
                corners.Add(projectedPoint);
            }

            _camera.transform.SetPositionAndRotation(originalPosition, originalRotation);

            return corners;
        }

        private Vector3[] GetViewportCorners(float nearClipPlane)
        {
            return new Vector3[]
            {
                new(0, 0, nearClipPlane),
                new(1, 0, nearClipPlane),
                new(1, 1, nearClipPlane),
                new(0, 1, nearClipPlane),
            };
        }

        private Vector3 ProjectPointOntoPlane(Vector3 point, Plane plane)
        {
            Ray ray = new(point, _camera.transform.forward);

            return plane.Raycast(ray, out float distance)
                ? ray.GetPoint(distance)
                : point;
        }
    }
}