using System;
using System.Collections.Generic;
using BattleBase.UpdateService;
using UnityEngine;

namespace BattleBase.Gameplay.CameraNavigation
{
    public class FrustumProjectionService : IFrustumProjectionService
    {
        private const float NearClipOffset = 0.01f;
        private const int CornerCount = 4;

        private static readonly Vector3[] s_viewportCorners = new Vector3[]
        {
            new(0, 0, NearClipOffset),
            new(1, 0, NearClipOffset),
            new(1, 1, NearClipOffset),
            new(0, 1, NearClipOffset),
        };

        private readonly Camera _camera;
        private readonly Transform _cameraTransform;
        private readonly ICameraAreaService _areaService;
        private readonly ICameraTracker _cameraTracker;

        public FrustumProjectionService(Camera camera, ICameraAreaService areaService, ICameraTracker cameraTracker)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _areaService = areaService ?? throw new ArgumentNullException(nameof(areaService));
            _cameraTracker = cameraTracker ?? throw new ArgumentNullException(nameof(cameraTracker));
            _cameraTransform = camera.transform;

            _areaService.Changed += OnAreaChanged;
            _cameraTracker.PositionChanged += OnPositionChanged;
            _cameraTracker.RotationChanged += OnRotationChanged;
            _cameraTracker.OrtoSizeChanged += OnOrtoSizeChanged;

            RefreshCache(_cameraTransform.position);
        }

        public event Action Changed;

        public IReadOnlyList<Vector3> Corners { get; private set; }

        public Vector3 ProjectedCenter { get; private set; }

        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners)
        {
            outCorners.Clear();

            Transform originalTransform = _cameraTransform;
            originalTransform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);
            originalTransform.position = cameraPosition;
            Plane plane = new(Vector3.up, _areaService.GroundPlaneY);

            foreach (Vector3 viewportCorner in s_viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Vector3 projected = ProjectPointOntoPlane(worldCorner, plane);
                outCorners.Add(projected);
            }

            originalTransform.SetPositionAndRotation(originalPosition, originalRotation);
        }

        private Vector3 ProjectPointOntoPlane(Vector3 point, Plane plane)
        {
            Ray ray = new(point, _cameraTransform.forward);

            return plane.Raycast(ray, out float distance) ? ray.GetPoint(distance) : point;
        }

        private void RefreshCache(Vector3 cameraPosition)
        {
            List<Vector3> cornersList = new(CornerCount);
            Transform originalTransform = _cameraTransform;
            originalTransform.GetPositionAndRotation(out Vector3 originalPosition, out Quaternion originalRotation);
            originalTransform.position = cameraPosition;
            Plane plane = new(Vector3.up, _areaService.GroundPlaneY);

            foreach (Vector3 viewportCorner in s_viewportCorners)
            {
                Vector3 worldCorner = _camera.ViewportToWorldPoint(viewportCorner);
                Vector3 projected = ProjectPointOntoPlane(worldCorner, plane);
                cornersList.Add(projected);
            }

            originalTransform.SetPositionAndRotation(originalPosition, originalRotation);
            Corners = cornersList.AsReadOnly();

            if (Corners.Count > 0)
            {
                Vector3 sum = Vector3.zero;

                foreach (var corner in Corners)
                    sum += corner;

                ProjectedCenter = sum / Corners.Count;
            }
            else
            {
                ProjectedCenter = Vector3.zero;
            }
        }

        private void OnAreaChanged()
        {
            RefreshCache(_cameraTransform.position);
            Changed?.Invoke();
        }

        private void OnPositionChanged()
        {
            RefreshCache(_cameraTransform.position);
            Changed?.Invoke();
        }

        private void OnRotationChanged()
        {
            RefreshCache(_cameraTransform.position);
            Changed?.Invoke();
        }

        private void OnOrtoSizeChanged()
        {
            RefreshCache(_cameraTransform.position);
            Changed?.Invoke();
        }
    }

    public class CameraTracker : ICameraTracker, IDisposable
    {
        private readonly Camera _camera;
        private readonly Transform _cameraTransform;
        private readonly IUpdater _updater;

        public CameraTracker(Camera camera, IUpdater updater)
        {
            _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));
            _updater = updater ?? throw new ArgumentNullException(nameof(updater));
            _cameraTransform = camera.transform;

            CashedPosition = _cameraTransform.position;
            CashedRotation = _cameraTransform.rotation;
            CashedOrtoSize = _camera.orthographicSize;

            _updater.Subscribe(OnUpdate, UpdateType.Update);
        }

        public event Action RotationChanged;
        public event Action PositionChanged;
        public event Action OrtoSizeChanged;

        public Vector3 CashedPosition { get; private set; }

        public Quaternion CashedRotation { get; private set; }

        public float CashedOrtoSize { get; private set; }

        public void Dispose() =>
            _updater?.Unsubscribe(OnUpdate, UpdateType.Update);

        private void OnUpdate()
        {
            if (_cameraTransform.rotation != CashedRotation)
            {
                CashedRotation = _cameraTransform.rotation;
                RotationChanged?.Invoke();
            }

            if (_cameraTransform.position != CashedPosition)
            {
                CashedPosition = _cameraTransform.position;
                PositionChanged?.Invoke();
            }

            if (_camera.orthographicSize != CashedOrtoSize)
            {
                CashedOrtoSize = _camera.orthographicSize;
                OrtoSizeChanged?.Invoke();
            }
        }
    }

    public interface ICameraTracker
    {
        public event Action RotationChanged;
        public event Action PositionChanged;
        public event Action OrtoSizeChanged;

        public Vector3 CashedPosition { get; }

        public Quaternion CashedRotation { get; }

        public float CashedOrtoSize { get; }
    }
}