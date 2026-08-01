using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class EconomyScoreModifier : IScoreModifier
    {
        private readonly Dictionary<TacticCategory, IModifier> _modifier;

        public EconomyScoreModifier(IScoreModifierConfig config)
        {
            
        }

        public int Modify(TacticCategory category, int score)
        {
            if (score <= 0)
                return 0;

            throw new System.Exception();
        }
    }
}