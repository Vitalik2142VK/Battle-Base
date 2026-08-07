using BattleBase.Gameplay.AI.Tactics;
using System;

namespace BattleBase.Gameplay.AI.Modifiers.Defense
{
    public class DefenseScoreModifier : IAdvancedScoreModifier
    {
        private readonly IAreaDefenseAI _areaDefenseAI;
        private readonly ScoreModifier _scoreModifier;

        public DefenseScoreModifier(IDefenseModifierConfig config, IAreaDefenseAI areaDefenseAI)
        {
            _areaDefenseAI = areaDefenseAI ?? throw new ArgumentNullException(nameof(areaDefenseAI));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _scoreModifier = new ScoreModifier(config.Modifiers);
        }

        public bool IsActivationNecessary()
        {
            throw new NotImplementedException();
        }

        public int Modify(TacticCategory category, int score)
        {
            return _scoreModifier.Modify(category, score);
        }
    }
}