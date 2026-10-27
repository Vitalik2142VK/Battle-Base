namespace BattleBase.Gameplay.AI.Modifiers.Univarsal
{
    public interface ITickScoreModifierConfig : IScoreModifierConfig
    {
        public int TickNumber { get; }
    }
}