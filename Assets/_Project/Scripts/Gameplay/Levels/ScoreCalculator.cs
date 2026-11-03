using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Map;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Levels
{
    public class ScoreCalculator : IScoreCalculator, IDisposable
    {
        private readonly IActorsControllerEvents _actorsControllerEvents;
        private readonly float _scoreCoefficient;

        private int _currentCount;

        public ScoreCalculator(IActorsControllerEvents actorsControllerEvents, ISelectedTerritory selectedTerritory)
        {
            if (selectedTerritory == null)
                throw new ArgumentNullException(nameof(selectedTerritory));

            _actorsControllerEvents = actorsControllerEvents ?? throw new ArgumentNullException(nameof(actorsControllerEvents));
            _actorsControllerEvents.DisabledActors += OnIncreaseCount;

            _scoreCoefficient = selectedTerritory.SelectedTerritoryInfo.UpgradeActrosCoefficient;
            _currentCount = 0;
        }

        public void Dispose()
        {
            _actorsControllerEvents.DisabledActors -= OnIncreaseCount; 
        }

        public int Calculate() =>
            (int)(_currentCount * _scoreCoefficient);

        private void OnIncreaseCount(IEnumerable<IActor> disabledActors)
        {
            if (disabledActors == null)
                throw new ArgumentNullException(nameof(disabledActors));

            foreach (var actor in disabledActors)
            {
                if (actor.TeamType == TeamType.Enemy)
                    _currentCount += actor.Data.Power;
            }
        }
    }
}