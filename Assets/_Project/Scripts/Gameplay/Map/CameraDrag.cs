using System;
using System.Collections.Generic;
using BattleBase.Abstract;
using BattleBase.InputSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    public class CameraDrag : MonoBehaviour, IInjectable
    {
        [Header("Drag Settings")]
        [SerializeField] private float _dragSpeed = 1f;
        [SerializeField] private bool _invertDrag = false;

        [Header("Boundaries (Collider)")]
        [SerializeField] private Collider _boundaryCollider;
        [SerializeField] private float _planeY = 0f;

        private IMapCameraInputReader _inputReader;
        private Vector3 _dragOrigin;
        private bool _isDragging = false;
        private Camera _camera;

        [Inject]
        public void Construct(IMapCameraInputReader inputReader) =>
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));

        private void Awake() =>
            _camera = Camera.main;

        private void Start()
        {
            if (_boundaryCollider != null)
                ApplyPositionConstraints();
        }

        private void Update()
        {
            HandleDrag();

            if (_boundaryCollider != null)
                ApplyPositionConstraints();
        }

        private void ApplyPositionConstraints()
        {
            Vector3 clampedPos = ClampToColliderBounds(transform.position);

            if (transform.position != clampedPos)
                transform.position = clampedPos;
        }

        private void HandleDrag()
        {
            //if (_isDragging && Input.GetMouseButton((int)dragButton))
            //{
            //    Vector3 currentPos = GetMouseWorldPosition();
            //    Vector3 delta = currentPos - _dragOrigin;
            //    float direction = _invertDrag ? 1f : -1f;
            //    Vector3 move = new Vector3(delta.x * direction, 0, delta.z * direction) * _dragSpeed;
            //    Vector3 newPos = transform.position + move;

            //    if (_boundaryCollider != null)
            //        newPos = ClampToColliderBounds(newPos);

            //    transform.position = newPos;
            //    _dragOrigin = GetMouseWorldPosition();
            //}
        }

        private List<Vector3> GetScreenCornersOnPlaneForPosition(Vector3 cameraPos)
        {
            List<Vector3> corners = new();
            Plane plane = new(Vector3.up, new Vector3(0, _planeY, 0));

            Vector3 originalPos = _camera.transform.position;
            _camera.transform.position = cameraPos;

            Ray[] rays = new Ray[4];
            rays[0] = _camera.ScreenPointToRay(new Vector3(0, 0, 0));
            rays[1] = _camera.ScreenPointToRay(new Vector3(Screen.width, 0, 0));
            rays[2] = _camera.ScreenPointToRay(new Vector3(0, Screen.height, 0));
            rays[3] = _camera.ScreenPointToRay(new Vector3(Screen.width, Screen.height, 0));

            foreach (Ray ray in rays)
            {
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 point = ray.GetPoint(distance);
                    corners.Add(IsValid(point) ? point : cameraPos);
                }
                else
                    corners.Add(cameraPos);
            }

            _camera.transform.position = originalPos;

            return corners;
        }

        private Vector3 ClampToColliderBounds(Vector3 desiredPosition)
        {
            if (!IsValid(desiredPosition))
                return transform.position;

            List<Vector3> corners = GetScreenCornersOnPlaneForPosition(desiredPosition);
            Bounds bounds = _boundaryCollider.bounds;
            Vector3 correction = Vector3.zero;

            foreach (Vector3 corner in corners)
            {
                if (bounds.Contains(corner) == false)
                {
                    if (corner.x < bounds.min.x) correction.x += corner.x - bounds.min.x;
                    if (corner.x > bounds.max.x) correction.x += corner.x - bounds.max.x;
                    if (corner.z < bounds.min.z) correction.z += corner.z - bounds.min.z;
                    if (corner.z > bounds.max.z) correction.z += corner.z - bounds.max.z;
                }
            }

            Vector3 newPos = desiredPosition - correction;
            newPos.y = desiredPosition.y;

            return IsValid(newPos) ? newPos : transform.position;
        }

        private bool IsValid(Vector3 v)
        {
            return float.IsNaN(v.x) == false && float.IsNaN(v.y) == false && float.IsNaN(v.z) == false &&
                   float.IsInfinity(v.x) == false && float.IsInfinity(v.y) == false && float.IsInfinity(v.z) == false;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new(Vector3.up, Vector3.zero);

            if (plane.Raycast(ray, out float distance))
                return ray.GetPoint(distance);

            return Vector3.zero;
        }
    }
}