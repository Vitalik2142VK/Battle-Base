namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IEconomyModifierConfig : IScoreModifierConfig
    {
        public int MinMaterialsForActivation { get; }
    }
}