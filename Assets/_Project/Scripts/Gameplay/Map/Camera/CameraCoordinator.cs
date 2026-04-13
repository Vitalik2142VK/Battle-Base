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
        [SerializeField][Min(0.01f)] private float _dragSpeed = 2f;
        [SerializeField][Min(0.01f)] private float _restoreSpeed = 3f;

        [Header("Zoom Settings")]
        [SerializeField] private float _zoomSpeed = 1f;
        [SerializeField] private float _minZoom = 0.7f;
        [SerializeField] private float _maxZoom = 1.2f;

        private IMapCameraInputReader _inputReader;
        private CameraFrustumProjector _frustumProjector;
        private CameraDragger _cameraDragger;
        private CameraZoom _cameraZoom;

        [Inject]
        public void Construct(IMapCameraInputReader inputReader) =>
            _inputReader = inputReader ?? throw new ArgumentNullException(nameof(inputReader));

        private void Awake() =>
            InitComponents();

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            _cameraDragger.Update(deltaTime, _inputReader?.DragDelta);
            _cameraZoom.Update(_inputReader?.ZoomDelta);
        }

        private void InitComponents()
        {
            _frustumProjector = GetComponent<CameraFrustumProjector>();
            _cameraDragger = new CameraDragger(transform, _frustumProjector, _dragSpeed, _restoreSpeed);
            _cameraZoom = new CameraZoom(Camera.main, _zoomSpeed, _minZoom, _maxZoom);
        }
    }
}