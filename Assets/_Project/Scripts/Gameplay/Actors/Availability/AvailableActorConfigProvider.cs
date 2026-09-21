using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Gameplay.Map;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class AvailableActorConfigProvider : IAvailableActorConfigProvider, IDisposable
    {
        private readonly IEnumerable<IActorConfig> _allCoonfigsInProject;
        private readonly TerritoriesModel _territoriesModel;

        private List<AvailableActorConfigModificator> _awailablePlayerConfigs;
        private List<AvailableActorConfigModificator> _awailableEnemyConfigs;

        public AvailableActorConfigProvider(IEnumerable<IActorConfig> allCoonfigsInProject, TerritoriesModel territoriesModel)
        {
            _allCoonfigsInProject = allCoonfigsInProject ?? throw new ArgumentNullException(nameof(allCoonfigsInProject));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));

            _territoriesModel.Changed += OnConquiredTerritroriesChanged;
            OnConquiredTerritroriesChanged();
        }

        public IEnumerable<IActorConfig> ActualPlayerConfigs => _awailablePlayerConfigs;

        public IEnumerable<IActorConfig> ActualEnemyConfigs => _awailableEnemyConfigs;

        public void Dispose() =>
            _territoriesModel.Changed -= OnConquiredTerritroriesChanged;

        private void RecombinedPlayerConfigs()
        {
            _awailablePlayerConfigs = new();

            foreach (var config in _allCoonfigsInProject)
            {
                AvailableActorConfigModificator newConfig = new(config);
                _awailablePlayerConfigs.Add(newConfig);
            }

            List<IActorConfig> actorsToOpen = new();

            foreach (int territoryIndex in _territoriesModel.ConqueredTerritories)
            {
                if (territoryIndex < 0)
                    continue;

                ITerritoryInfo info = _territoriesModel.GetTerritoryInfo(territoryIndex);
                actorsToOpen.AddRange(info.ActorsToOpen);

                foreach (var config in info.ActorsToOpen)
                    SetAwailableIfContains(config, _awailablePlayerConfigs);
            }

            _awailablePlayerConfigs = _awailablePlayerConfigs
                .Where(config => config.Data.IsAvailable)
                .ToList();
        }

        private void SetAwailableIfContains(IActorConfig config, List<AvailableActorConfigModificator> configs)
        {
            foreach (var playerConfig in configs)
            {
                if (playerConfig.Data.Id == config.Data.Id)
                    playerConfig.SetAvailable(true);
            }
        }

        private void OnConquiredTerritroriesChanged() =>
            RecombinedPlayerConfigs();
    }
}