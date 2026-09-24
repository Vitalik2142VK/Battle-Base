using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.Gameplay.Map;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class PlayerAvailableActorProvider : IPlayerAvailableActorProvider, IDisposable
    {
        private readonly IAllActorConfigs _allActorConfigs;
        private readonly TerritoriesModel _territoriesModel;

        private List<AvailableActorConfigModificator> _availablePlayerConfigs;

        public PlayerAvailableActorProvider(IAllActorConfigs allActorConfigs, TerritoriesModel territoriesModel)
        {
            _allActorConfigs = allActorConfigs ?? throw new ArgumentNullException(nameof(allActorConfigs));
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));

            _territoriesModel.Changed += OnConquiredTerritroriesChanged;
            OnConquiredTerritroriesChanged();
        }

        public IEnumerable<IActorConfig> ActualConfigs => _availablePlayerConfigs;

        public void Dispose() =>
            _territoriesModel.Changed -= OnConquiredTerritroriesChanged;

        private void RecombinedPlayerConfigs()
        {
            _availablePlayerConfigs = new();

            foreach (var config in _allActorConfigs.Configs)
            {
                AvailableActorConfigModificator newConfig = new(config);
                _availablePlayerConfigs.Add(newConfig);
            }

            List<IActorConfig> actorsToOpen = new();

            foreach (int territoryIndex in _territoriesModel.ConqueredTerritories)
            {
                if (territoryIndex < 0)
                    continue;

                ITerritoryInfo info = _territoriesModel.GetTerritoryInfo(territoryIndex);
                actorsToOpen.AddRange(info.ActorsToOpen);

                foreach (var config in info.ActorsToOpen)
                    SetAwailableIfContains(config, _availablePlayerConfigs);
            }

            _availablePlayerConfigs = _availablePlayerConfigs
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