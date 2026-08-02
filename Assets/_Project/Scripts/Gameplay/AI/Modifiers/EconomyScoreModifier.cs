using BattleBase.Gameplay.Actors.Economy;
using System;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public class EconomyScoreModifier : IAdvancedScoreModifier
    {
        private readonly IEconomyModifierConfig _config;
        private readonly IMaterialData _materialData;
        private readonly ScoreModifier _scoreModifier;

        public EconomyScoreModifier(IEconomyModifierConfig config, IMaterialData materialData)
        {
            _config = config ?? throw new ArgumentException(nameof(config));
            _materialData = materialData ?? throw new ArgumentException(nameof(materialData));

            _scoreModifier = new ScoreModifier(config.Modifiers);
        }

        public bool IsActivationNecessary() =>
            _materialData.CurrentMaterials < _config.MinMaterialsForActivation;

        public int Modify(TacticCategory category, int score) =>
            _scoreModifier.Modify(category, score);
    }
}