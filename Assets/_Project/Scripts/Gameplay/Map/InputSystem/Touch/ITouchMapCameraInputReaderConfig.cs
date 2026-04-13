namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface ITouchMapCameraInputReaderConfig
    {
        float DragSensitivity { get; }
        float ZoomSensitivity { get; }
        float DeadZone { get; }
        float MinPinchDistance { get; }
    }
}