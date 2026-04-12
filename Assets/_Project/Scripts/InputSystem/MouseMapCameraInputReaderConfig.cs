using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.InputSystem
{
    [CreateAssetMenu(
        fileName = nameof(MouseMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(MouseMapCameraInputReaderConfig))]
    public class MouseMapCameraInputReaderConfig : ScriptableObject, IMouseMapCameraInputReaderConfig
    {
        [SerializeField][Min(0.1f)] private float _mouseSensitivity = 1;
        [SerializeField][Min(0.1f)] public float _keyboardSensitivity = 1;

        public float MouseSensitivity => _mouseSensitivity;

        public float KeyboardSensitivity => _keyboardSensitivity;
    }
}