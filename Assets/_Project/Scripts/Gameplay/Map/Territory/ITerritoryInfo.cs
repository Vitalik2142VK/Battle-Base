using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI;
using BattleBase.Localization;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Map
{
    public interface ITerritoryInfo
    {
        public IActorConfig[] ActorsToOpen { get; }

        public IEnumerable<IActorConfig> BannedActors { get; }

        public ILanguageTextsSet TerritoryName { get; }

        public IBrainConfig BrainConfig { get; }

        public int CreditsForFirstVictory { get; }
    }
}