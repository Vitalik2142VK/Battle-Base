using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(Camera))]
    public class CameraFrustumProjector : MonoBehaviour, ICameraFrustumProjector
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraArea _area;

        private IFrustumProjectionService _projectionService;

        public ICameraArea Area => _area;

        public IReadOnlyList<Vector3> Corners
        {
            get
            {
                if (_projectionService == null)
                    InitComponents();

                return _projectionService.Corners;
            }
        }

        public Vector3 ProjectedCenter
        {
            get
            {
                if (_projectionService == null)
                    InitComponents();

                return _projectionService.ProjectedCenter;
            }
        }

        private void Awake() =>
            InitComponents();

        public void ProjectCornersOntoPlaneFromPosition(Vector3 cameraPosition, List<Vector3> outCorners)
        {
            if (_projectionService == null)
                InitComponents();

            _projectionService.ProjectCornersOntoPlaneFromPosition(cameraPosition, outCorners);
        }

        private void InitComponents()
        {
            if (_camera == null)
                throw new NullReferenceException(nameof(_camera));

            if (_area == null)
                throw new NullReferenceException(nameof(_area));

            _projectionService = new FrustumProjectionService(_camera, _area);
        }
    }
}