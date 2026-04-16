using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(TouchCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TouchCameraInputReaderConfig))]
    public class TouchCameraInputReaderConfig : ScriptableObject, ITouchCameraInputReaderConfig
    {
        [SerializeField][Min(0.0001f)] private float _zoomSensitivity = 0.002f;
        [SerializeField][Min(0f)] private float _minPinchDistance = 10f;
        [SerializeField][Min(0f)] private float _dragDeltaThreshold = 0.01f;

        public float ZoomSensitivity => _zoomSensitivity;

        public float MinPinchDistance => _minPinchDistance;

        public float DragDeltaThreshold => _dragDeltaThreshold;
    }
}