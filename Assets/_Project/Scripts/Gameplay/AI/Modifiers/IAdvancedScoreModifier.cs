namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IAdvancedScoreModifier : IScoreModifier
    {
        public int Priority { get; }

        public bool IsActivationNecessary();
    }
}