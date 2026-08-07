namespace BattleBase.Gameplay.AI.Modifiers.Defense
{
    public interface IDefenseModifierConfig : IScoreModifierConfig
    {
        public float ScoreCoefficientForActor {  get; }

        public float MaxCoefficient {  get; }

        public int MinActorsForAction { get; }
    }
}