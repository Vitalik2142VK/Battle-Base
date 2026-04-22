namespace BattleBase.Gameplay.Map
{
    public interface ICameraConfig
    {
        public float RestoreSpeed { get; }

        public float ZoomSpeed { get; }

        public float MinimumOrtoSize { get; }

        public float MaximumOrtoSize { get; }
    }
}