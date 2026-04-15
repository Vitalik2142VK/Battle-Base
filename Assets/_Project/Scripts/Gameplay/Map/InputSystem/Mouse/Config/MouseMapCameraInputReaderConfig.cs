using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(MouseMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(MouseMapCameraInputReaderConfig))]
    public class MouseMapCameraInputReaderConfig : ScriptableObject, IMouseMapCameraInputReaderConfig
    {
        [SerializeField][Min(0.1f)] private float _keyboardSpeed = 1;
        [SerializeField][Min(0.1f)] private float _zoomSensitivity = 1f;

        public float KeyboardSpeed => _keyboardSpeed;

        public float ZoomSensitivity => _zoomSensitivity;
    }
}