using BattleBase.Gameplay.Actors;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public interface IBrainConfing
    {
        public TeamType TeamType { get; }

        public IEnumerable<ITacticSetting> TacticSetting { get; }
    }
}