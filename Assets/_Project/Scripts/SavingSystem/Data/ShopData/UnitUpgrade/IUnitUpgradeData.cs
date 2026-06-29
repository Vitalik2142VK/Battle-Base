namespace BattleBase.SaveService
{
    public interface IUnitUpgradeData
    {
        public string Name { get; }

        public int DamageLevel { get; }

        public int ArmorLevel { get; }

        public int BuildTimeLevel { get; }
    }
}