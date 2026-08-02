namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IAdvancedScoreModifier : IScoreModifier
    {
        public bool IsActivationNecessary();
    }
}