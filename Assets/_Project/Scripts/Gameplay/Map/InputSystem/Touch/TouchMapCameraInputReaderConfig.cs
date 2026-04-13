using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(TouchMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TouchMapCameraInputReaderConfig))]
    public class TouchMapCameraInputReaderConfig : ScriptableObject, ITouchMapCameraInputReaderConfig
    {
        [SerializeField][Min(0.01f)] private float _dragSensitivity = 1f;
        [SerializeField][Min(0.01f)] private float _zoomSensitivity = 0.1f;
        [SerializeField][Min(0f)] private float _deadZone = 5f;
        [SerializeField][Min(0f)] private float _minPinchDistance = 10f;

        public float DragSensitivity => _dragSensitivity;

        public float ZoomSensitivity => _zoomSensitivity;

        public float DeadZone => _deadZone;

        public float MinPinchDistance => _minPinchDistance;
    }
}