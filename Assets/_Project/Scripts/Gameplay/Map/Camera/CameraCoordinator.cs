using System;
using BattleBase.Abstract;
using BattleBase.Gameplay.Map.InputSystem;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Map
{
    [RequireComponent(typeof(CameraFrustumProjector))]
    public class CameraCoordinator : MonoBehaviour, IInjectable
    {
        [Header("Drag Settings")]
        [SerializeField][Min(0.01f)] private float _restoreSpeed = 3f;
        [SerializeField] private bool _isDynamicAngle;

        [Header("Zoom Settings")]
        [SerializeField] private float _zoomSpeed = 1f;
        [SerializeField] private float _minZoom = 0.4f;
        [SerializeField] private float _maxZoom = 1.3f;

        private IMapCameraInputReader _inputReader;
        private CameraFrustumProjector _frustumProjector;
        private CameraDragger _cameraDragger;
        private CameraZoom _cameraZoom;
        private float _zCompensation;

        [Inject]
        public void Construct(IMapCameraInputReader inputReader) =>
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));

        private void Awake()
        {
            InitComponents();
            UpdateCompensation();
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            Vector3? dragDelta = _inputReader?.WorldDragDelta;

            if (dragDelta.HasValue)
            {
                Vector3 delta = dragDelta.Value;

                if (_isDynamicAngle)
                    UpdateCompensation();

                delta.z *= _zCompensation;
                dragDelta = delta;
            }

            _cameraDragger.Update(deltaTime, dragDelta);
            _cameraZoom.Update(_inputReader?.ZoomDelta);
        }

        private void InitComponents()
        {
            _frustumProjector = GetComponent<CameraFrustumProjector>();
            _cameraDragger = new CameraDragger(Camera.main, _frustumProjector, _restoreSpeed);
            _cameraZoom = new CameraZoom(Camera.main, _zoomSpeed, _minZoom, _maxZoom);
        }

        private void UpdateCompensation()
        {
            float angleX = Mathf.Abs(Camera.main.transform.eulerAngles.x);
            float sin = Mathf.Sin(angleX * Mathf.Deg2Rad);
            _zCompensation = sin > 0.001f ? 1f / sin : 1f;
        }
    }
}