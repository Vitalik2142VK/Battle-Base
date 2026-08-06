namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticSetting
    {
        public TacticCategory Category { get; }

        public int MaxScore { get; }

        public int MinScore { get; }
    }
}