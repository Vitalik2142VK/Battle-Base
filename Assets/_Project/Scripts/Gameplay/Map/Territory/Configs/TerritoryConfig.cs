using BattleBase.Gameplay.Actors;
using BattleBase.Localization;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(TerritoryConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(TerritoryConfig))]
    public class TerritoryConfig : ScriptableObject, ITerritoryInfo
    {
        [SerializeField] private LanguageTextsSet _territoryName;
        [SerializeField][Min(0)] private int _creditsForFirstVictory;
        [SerializeField] private ActorConfig[] _actorsToOpen;
        [SerializeField] private ActorConfig[] _bannedActors;

        public IActorConfig[] ActorsToOpen => _actorsToOpen;

        public IEnumerable<IActorConfig> BannedActors => _bannedActors;

        public ILanguageTextsSet TerritoryName => _territoryName;

        public int CreditsForFirstVictory => _creditsForFirstVictory;
    }
}