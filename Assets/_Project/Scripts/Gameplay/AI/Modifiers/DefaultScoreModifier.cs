namespace BattleBase.Gameplay.AI.Modifiers
{
    public class DefaultScoreModifier : IScoreModifier
    {
        public int Modify(TacticCategory _, int score)
        {
            if (score <= 0)
                return 0;

            return score;
        }
    }
}