namespace BattleBase.Gameplay.AI
{
    public interface ITacticSetting
    {
        public TacticCategory Category { get; }

        public int Score { get; }
    }
}