using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Map;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class EnemyAvailableActorProvider : IEnemyAvailableActorProvider
    {
        private readonly IAllActorConfigs _allActorConfigs;
        private readonly ISelectedTerritory _selectedTerritory;

        public EnemyAvailableActorProvider(
            IAllActorConfigs allActorConfigs,
            ISelectedTerritory selectedTerritory)
        {
            _allActorConfigs = allActorConfigs ?? throw new ArgumentNullException(nameof(allActorConfigs));
            _selectedTerritory = selectedTerritory ?? throw new ArgumentNullException(nameof(selectedTerritory));
        }

        public IEnumerable<IActorConfig> ActualConfigs => GetActorConfigs();

        private IEnumerable<IActorConfig> GetActorConfigs()
        {
            Dictionary<string, AvailableActorConfigModificator> availablePlayerConfigs = new();
            IEnumerable<IActorConfig> bannedActors = _selectedTerritory.SelectedTerritoryInfo.BannedActors;

            foreach (var actorConfig in _allActorConfigs.Configs)
            {
                AvailableActorConfigModificator modificator = new(actorConfig);
                modificator.SetAvailable(true);
                availablePlayerConfigs.Add(actorConfig.Data.Id, modificator);
            }

            foreach (var actorConfig in bannedActors)
                availablePlayerConfigs[actorConfig.Data.Id].SetAvailable(false);

            return availablePlayerConfigs.Values;
        }
    }
}