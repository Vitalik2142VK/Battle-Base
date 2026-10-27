using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Attack
{
    public interface ISpawnUnitsTacticSetting : ITacticSetting
    {
        public IEnumerable<string> UnitFactoriesIds { get; }

        public IEnumerable<string> ForbiddenUnitsIds { get; }

        public int MaxNumSpawn { get; }

        public int MinNumSpawn { get; }
    }
}