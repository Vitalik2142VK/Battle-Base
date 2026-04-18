namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface ITouchConfig
    {
        float ZoomSensitivity { get; }

        float MinPinchDistance { get; }

        float DragDeltaThreshold { get; }
    }
}