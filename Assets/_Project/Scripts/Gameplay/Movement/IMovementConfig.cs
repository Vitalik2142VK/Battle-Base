namespace BattleBase.Gameplay.Movement
{
    public interface IMovementConfig
    {
        public float Speed { get; }

        public float AngularSpeed { get; }

        public float Acceleration { get; }

        public float StoppingDistance { get; }

        public float DistanceFinish { get; }
    }
}
