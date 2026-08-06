namespace BattleBase.Gameplay.AI.Modifiers.Economy
{
    public interface IEconomyModifierConfig : IScoreModifierConfig
    {
        public int MinMaterialsForActivation { get; }
    }
}