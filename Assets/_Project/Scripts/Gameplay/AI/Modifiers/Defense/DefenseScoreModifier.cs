using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Tactics;
using System;

namespace BattleBase.Gameplay.AI.Modifiers.Defense
{
    public class DefenseScoreModifier : IAdvancedScoreModifier
    {
        private readonly IDefenseModifierConfig _config;
        private readonly IBuildingSitesController _sitesController;
        private readonly IAreaDefenseAI _areaDefenseAI;
        private readonly ScoreModifier _scoreModifier;

        public DefenseScoreModifier(
            IDefenseModifierConfig config,
            IBuildingSitesController defenseSitesController,
            IAreaDefenseAI areaDefenseAI)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _sitesController = defenseSitesController ?? throw new ArgumentNullException(nameof(defenseSitesController));
            _areaDefenseAI = areaDefenseAI ?? throw new ArgumentNullException(nameof(areaDefenseAI));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            _scoreModifier = new ScoreModifier(config.Modifiers);
        }

        public bool IsActivationNecessary() =>
            _sitesController.HasFreeSites && _areaDefenseAI.GetNumberActorsInArea() > _config.MinActorsForAction;

        public int Modify(TacticCategory category, int score)
        {
            int resultScore = _scoreModifier.Modify(category, score);
            float scoreCoefficient = _config.ScoreCoefficientForActor * _areaDefenseAI.GetNumberActorsInArea();

            if (scoreCoefficient > _config.MaxCoefficient)
                scoreCoefficient = _config.MaxCoefficient;

            resultScore = (int)(resultScore * scoreCoefficient);

            return resultScore;
        }
    }
}