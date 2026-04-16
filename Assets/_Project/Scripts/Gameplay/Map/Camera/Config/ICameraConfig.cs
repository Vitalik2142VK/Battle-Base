namespace BattleBase.Gameplay.Map
{
    public interface ICameraConfig
    {
        public float RestoreSpeed { get; }

        public float ZoomSpeed { get; }

        public float MinimumZoom { get; }

        public float MaximumZoom { get; }
    }
}