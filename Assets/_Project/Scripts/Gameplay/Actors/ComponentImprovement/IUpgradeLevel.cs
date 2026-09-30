namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public interface IUpgradeLevel
    {
        public int MaximumLevel { get; }

        public int CurrentLevel { get; }
    }
}