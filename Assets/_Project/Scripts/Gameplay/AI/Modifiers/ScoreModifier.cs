using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class ScoreModifier
    {
        private readonly Dictionary<TacticCategory, IModifier> _modifier;

        public ScoreModifier(IEnumerable<IModifier> modifiers)
        {
            if (modifiers == null)
                throw new System.ArgumentNullException(nameof(modifiers));

            _modifier = new Dictionary<TacticCategory, IModifier>();

            foreach (var modifier in modifiers)
                _modifier.Add(modifier.Category, modifier);
        }

        public int Modify(TacticCategory category, int score)
        {
            if (score <= 0)
                return 0;

            if (_modifier.ContainsKey(category) == false)
                return score;

            return (int)(score * _modifier[category].Multiplier);
        }
    }
}