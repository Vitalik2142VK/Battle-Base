using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Map;

namespace BattleBase
{
    public class AvailableActorConfigProvider : IDisposable
    {
        private readonly IEnumerable<IActorConfig> _allCoonfigsInProject;
        private readonly TerritoriesModel _territoriesModel;

        private List<MutationActorConfig> _awailablePlayerConfigs;
        private List<MutationActorConfig> _awailableEnemyConfigs;

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

            foreach (IActorConfig config in _allCoonfigsInProject)
            {
                MutationActorConfig newConfig = new(config);
                _awailablePlayerConfigs.Add(newConfig);
            }

            List<IActorConfig> actorsToOpen = new();

            foreach (int territoryIndex in _territoriesModel.ConqueredTerritories)
            {
                if (territoryIndex < 0)
                    continue;

                ITerritoryInfo info = _territoriesModel.GetTerritoryInfo(territoryIndex);
                actorsToOpen.AddRange(info.ActorsToOpen);

                foreach (IActorConfig config in info.ActorsToOpen)
                    SetAwailableIfContains(config, _awailablePlayerConfigs);
            }

            _awailablePlayerConfigs = _awailablePlayerConfigs
                .Where(config => config.Data.IsAvailable)
                .ToList();
        }

        private void SetAwailableIfContains(IActorConfig config, List<MutationActorConfig> configs)
        {
            foreach (MutationActorConfig playerConfig in configs)
            {
                if (playerConfig.Data.Id == config.Data.Id)
                    playerConfig.SetAvailable(true);
            }
        }

        private void OnConquiredTerritroriesChanged() =>
            RecombinedPlayerConfigs();
    }
}