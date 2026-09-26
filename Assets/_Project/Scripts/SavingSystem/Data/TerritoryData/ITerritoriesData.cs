using System.Collections.Generic;

namespace BattleBase.SaveService
{
    public interface ITerritoriesData : IChangeTrackable<ITerritoriesData>
    {
        public IReadOnlyList<int> ConqueredTerritoryIndices { get; }

        public int SelectedTerritoryIndex { get; }
    }
}