using System.Collections.Generic;
using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class CameraFrustumProjector : MonoBehaviour
    {
        private const float SphereRadius = 0.1f;
        private const float NearClipOffset = 0.01f;
        private const int CornerCount = 4;

        [SerializeField] private CameraArea _area;
        [SerializeField] private Color _gizmoColor = Color.yellow;

        private Camera _camera;

        public CameraArea Area => _area;

        private void OnDrawGizmos()
        {
            if (_camera == null || _area == null)
                FindComponents();

            Gizmos.color = _gizmoColor;
            List<Vector3> corners = GetCornersOnPlane(_camera.transform.position);

            if (corners.Count == CornerCount)
            {
                foreach (Vector3 corner in corners)
                    Gizmos.DrawWireSphere(corner, SphereRadius);

                for (int i = 0; i < corners.Count; i++)
                    Gizmos.DrawLine(corners[i], corners[(i + 1) % corners.Count]);
            }
        }

        private void Awake() =>
            FindComponents();

        public List<Vector3> GetCornersOnPlane(Vector3 cameraPosition)
        {
            List<Vector3> corners = new();
            Plane plane = new(Vector3.up, new Vector3(0, _area.PlaneY, 0));

            Vector3 originalPos = _camera.transform.position;
            _camera.transform.position = cameraPosition;
            float nearClipPlane = _camera.nearClipPlane + NearClipOffset;

            Vector3[] viewportCorners = new Vector3[]
            {
                new (0, 0, nearClipPlane),
                new (1, 0, nearClipPlane),
                new (1, 1, nearClipPlane),
                new (0, 1, nearClipPlane),
            };

            foreach (Vector3 viewportCorner in viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Ray ray = new(worldCorner, _camera.transform.forward);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 point = ray.GetPoint(distance);
                    corners.Add(VectorValidation.IsValid(point) ? point : worldCorner);
                }
                else
                {
                    corners.Add(worldCorner);
                }
            }

            _camera.transform.position = originalPos;

            return corners;
        }

        private void FindComponents()
        {
            _area = FindObjectOfType<CameraArea>();
            _camera = Camera.main;
        }
    }
}