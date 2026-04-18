namespace BattleBase.Gameplay.Map.InputSystem
{
    public interface IDragConfig
    {
        public float KeyboardSpeed { get; }

        public float DragDeltaThreshold { get; }

        public float KeyboardAxisThreshold { get; }
    }
}