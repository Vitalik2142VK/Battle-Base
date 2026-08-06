using BattleBase.Gameplay.Actors.Energy;
using BattleBase.Gameplay.AI.Tactics;
using System;

namespace BattleBase.Gameplay.AI.Modifiers.Energy
{
    public class PowerScoreModifier : IAdvancedScoreModifier
    {
        private readonly IPowerModifierConfig _config;
        private readonly IPowerData _powerData;
        private readonly ScoreModifier _scoreModifier;

        public PowerScoreModifier(IPowerModifierConfig config, IPowerData powerData)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _powerData = powerData ?? throw new ArgumentNullException(nameof(powerData));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _scoreModifier = new ScoreModifier(config.Modifiers);
        }

        public bool IsActivationNecessary() =>
            _powerData.HasMaxCapacity == false && _powerData.FreeEnergy < _config.MaxRemainingEnergy;

        public int Modify(TacticCategory category, int score) =>
            _scoreModifier.Modify(category, score);
    }
}