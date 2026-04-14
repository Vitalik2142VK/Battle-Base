using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(TouchMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TouchMapCameraInputReaderConfig))]
    public class TouchMapCameraInputReaderConfig : ScriptableObject, ITouchMapCameraInputReaderConfig
    {
        [SerializeField][Min(0.0001f)] private float _zoomSensitivity = 0.004f;
        [SerializeField][Min(0f)] private float _minPinchDistance = 10f;

        public float ZoomSensitivity => _zoomSensitivity;

        public float MinPinchDistance => _minPinchDistance;
    }
}