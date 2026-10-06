using BattleBase.Gameplay.CameraNavigation;
using BattleBase.Utils.Constants;
using BattleBase.Utils.Extensions;
using System;
using UnityEngine;
using VContainer;

namespace BattleBase.Gameplay.Audio
{
    [RequireComponent(typeof(AudioListener))]
    public class AudioListnerPositionSetter : MonoBehaviour
    {
        [SerializeField][Range(0, 1f)] private float _downOffsetCoefficient = 0.5f;
        [SerializeField][Min(1f)] private float _maxHeight = 60f;
        [SerializeField][Min(0f)] private float _minHeight = 10f;

        private IFrustumProjectionService _frustumProjectionService;
        private ICameraOrientationAdapter _cameraOrientationAdapter;
        private Transform _transform;

        private float Height => _cameraOrientationAdapter.CurrentSize.Remap(
                _cameraOrientationAdapter.MinimumSize,
                _cameraOrientationAdapter.MaximumSize,
                _minHeight,
                _maxHeight);

        [Inject]
        public void Construct(
            IFrustumProjectionService frustumProjectionService,
            ICameraOrientationAdapter cameraOrientationAdapter)
        {
            _frustumProjectionService = frustumProjectionService ?? throw new ArgumentNullException(nameof(frustumProjectionService));
            _cameraOrientationAdapter = cameraOrientationAdapter ?? throw new ArgumentNullException(nameof(cameraOrientationAdapter));
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _frustumProjectionService.Changed += OnUpdatePosition;

            OnUpdatePosition();
        }

        private void OnDisable()
        {
            _frustumProjectionService.Changed -= OnUpdatePosition;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 3f);
        }

        private void OnUpdatePosition()
        {
            FrustumProjection projection = _frustumProjectionService.Projection;
            Vector3 down = Vector3.Lerp(projection.RightDown, projection.LeftDown, Values.Half);
            Vector3 position = Vector3.Lerp(projection.Center, down, _downOffsetCoefficient);
            position.y = Height;
            _transform.position = position;
        }
    }
}