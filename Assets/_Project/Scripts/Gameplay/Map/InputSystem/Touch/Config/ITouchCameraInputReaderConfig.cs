namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface ITouchCameraInputReaderConfig
    {
        float ZoomSensitivity { get; }

        float MinPinchDistance { get; }

        float DragDeltaThreshold { get; }
    }
}