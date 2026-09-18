using BattleBase.Gameplay.Actors;
using BattleBase.Localization;

namespace BattleBase.Gameplay.Map
{
    public interface ITerritoryInfo
    {
        public ILanguageTextsSet TerritoryName { get; }

        public int CreditsForFirstVictory { get; }

        public IActorConfig[] ActorsToOpen { get; }
    }
}