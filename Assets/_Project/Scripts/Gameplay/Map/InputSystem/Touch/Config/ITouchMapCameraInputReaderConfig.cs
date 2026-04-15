namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface ITouchMapCameraInputReaderConfig
    {
        float ZoomSensitivity { get; }

        float MinPinchDistance { get; }
    }
}