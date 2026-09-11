using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Gameplay.AI.Tactics;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public interface IBrainConfig
    {
        public TeamType TeamType { get; }

        public IEnumerable<ITacticSetting> TacticSetting { get; }

        public IEnumerable<IScoreModifierConfig> ScoreModifierConfigs { get; }
    }
}