namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IUpgraderConfig
    {
        public float DamageCoefficientByLevel { get; }

        public float HealtheCoefficientByLevel { get; }
    }
}