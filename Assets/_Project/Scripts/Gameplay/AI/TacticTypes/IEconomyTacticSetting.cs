namespace BattleBase.Gameplay.AI.TacticTypes
{
    public interface IEconomyTacticSetting : ITacticSetting
    {
        public string MaterialFactoryId { get; }

        public int[] LineNumbersForBuild { get; }

        public int ScoreForAction { get; }

        public int MaterialsForStop { get; }

        public int MaxFactories { get; }
    }
}