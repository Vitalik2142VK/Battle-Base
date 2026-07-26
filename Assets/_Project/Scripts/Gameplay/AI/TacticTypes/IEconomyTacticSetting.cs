namespace BattleBase.Gameplay.AI.TacticTypes
{
    public interface IEconomyTacticSetting : ITacticSetting
    {
        public string MaterialFactoryId { get; }

        public int[] LineNumbersForBuild { get; }

        public int MaxFactories { get; }

        public int MaterialsForStop { get; }

        public int NumberActionsRow { get; }
    }
}