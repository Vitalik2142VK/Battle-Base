using BattleBase.Gameplay.AI.Tactics;

namespace BattleBase.Gameplay.AI.Modifiers.Univarsal
{
    public class TickScoreModifier : IAdvancedScoreModifier
    {
        private readonly ITickScoreModifierConfig _config;
        private readonly ScoreModifier _scoreModifier;

        private int _currentTick;

        public TickScoreModifier(ITickScoreModifierConfig config)
        {
            _config = config ?? throw new System.ArgumentNullException(nameof(config));

            _scoreModifier = new ScoreModifier(_config.Modifiers);
            _currentTick = 0;
        }

        public int Priority => _config.Priority;

        public bool IsActivationNecessary()
        {
            if (++_currentTick > _config.TickNumber)
                _currentTick = _config.TickNumber;

            return _currentTick == _config.TickNumber;
        }

        public int Modify(TacticCategory category, int score)
        {
            _currentTick = 0;

            return _scoreModifier.Modify(category, score);
        }
    }
}