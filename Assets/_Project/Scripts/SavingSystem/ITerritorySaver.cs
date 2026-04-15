using System.Collections.Generic;

namespace BattleBase.SaveService
{
    public interface ITerritorySaver : ISaver
    {
        public IReadOnlyList<int> ConqueredTerritories { get; }

        public void SetConqueredTerritories(IReadOnlyList<int> territories);
    }
}