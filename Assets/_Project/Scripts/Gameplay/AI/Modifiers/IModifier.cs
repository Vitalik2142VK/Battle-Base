namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IModifier
    {
        public TacticCategory Category { get; }

        public float Multiplier { get; }
    }
}