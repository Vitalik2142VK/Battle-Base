using BattleBase.Gameplay.Actors;
using BattleBase.Localization;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Map
{
    public interface ITerritoryInfo
    {
        public IActorConfig[] ActorsToOpen { get; }

        public IEnumerable<IActorConfig> BannedActors { get; }

        public ILanguageTextsSet TerritoryName { get; }

        public int CreditsForFirstVictory { get; }
    }
}