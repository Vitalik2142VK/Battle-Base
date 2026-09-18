using System;
using System.Collections.Generic;
using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Map;
using BattleBase.SaveService;

namespace BattleBase.ShopSystem
{
    public class ActorConfigSource
    {
        private readonly IEnumerable<IActorConfig> _allCoonfigsInProject;
        private readonly IEnumerable<IActorConfig> _awailablePlayerConfigs;
        private readonly IEnumerable<IActorConfig> _awailableEnemyConfigs;
        private readonly IEnumerable<ITerritoryInfo> _territoriesInfos;
        private readonly ITerritorySaver _territorySaver;

        public ActorConfigSource(IEnumerable<IActorConfig> allCoonfigsInProject)
        {
            _allCoonfigsInProject = allCoonfigsInProject ?? throw new ArgumentNullException(nameof(allCoonfigsInProject));

        }

        private void RecombinedConfigs()
        {

        }
    }
}