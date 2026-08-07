using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Tactics.Economy
{
    public interface IEconomyTacticSetting : ITacticSetting
    {
        public string MaterialFactoryId { get; }

        public IEnumerable<int> LineNumbersForBuild { get; }

        public int ScoreForBuildFactory { get; }

        public int MaterialsForStop { get; }

        public int MaxFactories { get; }
    }
}