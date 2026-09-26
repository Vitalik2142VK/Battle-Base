using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.SaveService
{
    [Serializable]
    public class TerritoriesData : ITerritoriesData
    {
        [SerializeField] private List<int> _conqueredTerritoryIndices = new();
        [SerializeField] private int _selectedTerritoryIndex = 0;

        public TerritoriesData() { }

        public TerritoriesData(ITerritoriesData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _conqueredTerritoryIndices = new(data.ConqueredTerritoryIndices);
            _selectedTerritoryIndex = data.SelectedTerritoryIndex;
        }

        public TerritoriesData(List<int> conqueredTerritories, int selectedTerritory = 0)
        {
            _conqueredTerritoryIndices = conqueredTerritories ?? throw new ArgumentNullException(nameof(conqueredTerritories));
            _selectedTerritoryIndex = selectedTerritory;
        }

        public IReadOnlyList<int> ConqueredTerritoryIndices => _conqueredTerritoryIndices;

        public int SelectedTerritoryIndex => _selectedTerritoryIndex;

        public bool IsTerritoryConquered(int index) =>
            _conqueredTerritoryIndices.Contains(index);

        public void SetData(ITerritoriesData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _conqueredTerritoryIndices = new(data.ConqueredTerritoryIndices);
            _selectedTerritoryIndex = data.SelectedTerritoryIndex;
        }

        public void SetSelectedTerritoryIndex(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Value must be positive");

            _selectedTerritoryIndex = index;
        }

        public bool TryMarkTerritoryConquered(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Value must be positive");

            if (_conqueredTerritoryIndices.Contains(index) == false)
            {
                _conqueredTerritoryIndices.Add(index);

                return true;
            }

            return false;
        }

        public bool IsDiffersFrom(ITerritoriesData other)
        {
            if (other == null)
                return true;

            IReadOnlyList<int> current = ConqueredTerritoryIndices;
            IReadOnlyList<int> newList = other.ConqueredTerritoryIndices;

            if (current.Count != newList.Count)
                return true;

            for (int i = 0; i < current.Count; i++)
            {
                if (current[i] != newList[i])
                    return true;
            }

            if (_selectedTerritoryIndex != other.SelectedTerritoryIndex)
                return true;

            return false;
        }
    }
}