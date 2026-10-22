#if UNITY_EDITOR
namespace BattleBase.Gameplay.Actors.ComponentImprovement
{
    public class MaxUpgradeLevel : IUpgradeLevel
    {
        private readonly IUpgradeLevel _upgradeLevel;

        public MaxUpgradeLevel(IUpgradeLevel upgradeLevel)
        {
            _upgradeLevel = upgradeLevel ?? throw new System.ArgumentNullException(nameof(upgradeLevel));
        }

        public int MaximumLevel => _upgradeLevel.MaximumLevel;

        public int CurrentLevel => MaximumLevel;
    }
}
#endif