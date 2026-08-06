using BattleBase.Gameplay.AI.Tactics;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IScoreModifier
    {
        public int Modify(TacticCategory category, int score);
    }
}