namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IMouseMapCameraInputReaderConfig
    {
        public float MouseSensitivity { get; }

        public float KeyboardSensitivity { get; }

        public float ZoomSensitivity { get; }
    }
}