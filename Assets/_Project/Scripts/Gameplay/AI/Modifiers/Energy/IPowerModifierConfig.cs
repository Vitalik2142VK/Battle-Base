namespace BattleBase.Gameplay.AI.Modifiers.Energy
{
    public interface IPowerModifierConfig : IScoreModifierConfig
    {
        public int MaxRemainingEnergy { get; }
    }
}