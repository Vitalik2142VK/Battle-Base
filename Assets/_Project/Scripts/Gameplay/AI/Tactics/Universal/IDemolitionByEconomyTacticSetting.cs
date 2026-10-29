namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    public interface IDemolitionByEconomyTacticSetting : ITacticSetting
    {
        public string BuildingId { get; }

        public int NumberRemainingFactories { get; }

        public int MaterialsForStart { get; }

        public bool IsRemoveAfterComplited { get; }
    }
}