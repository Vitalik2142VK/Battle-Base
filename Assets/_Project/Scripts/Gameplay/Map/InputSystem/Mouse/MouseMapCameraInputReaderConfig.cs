using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(MouseMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(MouseMapCameraInputReaderConfig))]
    public class MouseMapCameraInputReaderConfig : ScriptableObject, IMouseMapCameraInputReaderConfig
    {
        [SerializeField][Min(0.1f)] private float _mouseSensitivity = 1;
        [SerializeField][Min(0.1f)] private float _keyboardSensitivity = 1;
        [SerializeField][Min(0.1f)] private float _zoomSensitivity = 1f;

        public float MouseSensitivity => _mouseSensitivity;

        public float KeyboardSensitivity => _keyboardSensitivity;

        public float ZoomSensitivity => _zoomSensitivity;
    }
}