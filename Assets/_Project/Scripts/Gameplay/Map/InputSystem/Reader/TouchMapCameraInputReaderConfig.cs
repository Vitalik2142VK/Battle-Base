using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    [CreateAssetMenu(
<<<<<<<< HEAD:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/Config/TouchInputConfig.cs
        fileName = nameof(TouchInputConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TouchInputConfig))]
    public class TouchInputConfig : ScriptableObject, ITouchConfig, IClickConfig
========
        fileName = nameof(TouchMapCameraInputReaderConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TouchMapCameraInputReaderConfig))]
    public class TouchMapCameraInputReaderConfig : ScriptableObject, ITouchMapCameraInputReaderConfig
>>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/TouchMapCameraInputReaderConfig.cs
    {
        [SerializeField][Min(0.0001f)] private float _zoomSensitivity = 0.004f;
        [SerializeField][Min(0f)] private float _minPinchDistance = 10f;
<<<<<<<< HEAD:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/Config/TouchInputConfig.cs
        [SerializeField][Min(0f)] private float _dragDeltaThreshold = 0.01f;
        [SerializeField][Min(0f)] private float _clickDragThreshold = 5f;
========
>>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/TouchMapCameraInputReaderConfig.cs

        public float ZoomSensitivity => _zoomSensitivity;

        public float MinPinchDistance => _minPinchDistance;
<<<<<<<< HEAD:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/Config/TouchInputConfig.cs

        public float DragDeltaThreshold => _dragDeltaThreshold;

        public float ClickDragThreshold => _clickDragThreshold;
========
>>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96:Assets/_Project/Scripts/Gameplay/Map/InputSystem/Reader/TouchMapCameraInputReaderConfig.cs
    }
}