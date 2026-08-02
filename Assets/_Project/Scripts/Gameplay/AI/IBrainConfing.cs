using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public interface IBrainConfing
    {
        public TeamType TeamType { get; }

        public IEnumerable<ITacticSetting> TacticSetting { get; }

        public IEnumerable<IScoreModifierConfig> ScoreModifierConfigs { get; }
    }
}