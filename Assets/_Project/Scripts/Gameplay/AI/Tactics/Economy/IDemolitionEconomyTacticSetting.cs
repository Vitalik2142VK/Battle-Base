namespace BattleBase.Gameplay.AI.Tactics.Economy
{
    public interface IDemolitionEconomyTacticSetting : ITacticSetting
    {
        public string MaterialFactoryId { get; }

        public int NumberRemainingFactories { get; }

        public int MaterialsForStart { get; }
    }
}