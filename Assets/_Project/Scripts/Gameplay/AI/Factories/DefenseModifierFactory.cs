using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Modifiers.Defense;
using System;

namespace BattleBase.Gameplay.AI.Factories
{
    public class DefenseModifierFactory : IScoreModifierFactory
    {
        private readonly IAreaDefenseAI _areaDefenseAI;
        private readonly IBuildingSitesStorage _sitesStorage;

        public DefenseModifierFactory(IAreaDefenseAI areaDefenseAI, IBuildingSitesStorage sitesStorage)
        {
            _areaDefenseAI = areaDefenseAI ?? throw new ArgumentNullException(nameof(areaDefenseAI));
            _sitesStorage = sitesStorage ?? throw new ArgumentNullException(nameof(sitesStorage));
        }

        public ModifierType Type => ModifierType.Defense;

        public IAdvancedScoreModifier Create(IScoreModifierConfig configs, TeamType team)
        {
            if (configs == null)
                throw new ArgumentNullException(nameof(configs));

            if (configs is IDefenseModifierConfig defenseModifierConfig == false)
                throw new InvalidOperationException($"{nameof(configs)} is not implemented '{nameof(IDefenseModifierConfig)}'");

            IBuildingSitesController controller = _sitesStorage.GetBuildingSitesController(team, SiteType.Defense);

            return new DefenseScoreModifier(defenseModifierConfig, controller, _areaDefenseAI);
        }
    }
}