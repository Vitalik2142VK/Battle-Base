using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.SaveService
{
    [Serializable]
    public class TerritoryData : ITerritoryData
    {
        [SerializeField] private List<int> _conqueredTerritories = new() { 0, };
        [SerializeField] private int _selectedTerritory = -1;

        public TerritoryData() { }

        public TerritoryData(List<int> conqueredTerritories, int selectedTerritory = -1)
        {
            _conqueredTerritories = conqueredTerritories ?? throw new ArgumentNullException(nameof(conqueredTerritories));
            _selectedTerritory = selectedTerritory;
        }

        public IReadOnlyList<int> ConqueredTerritories => _conqueredTerritories;

        public int SelectedTerrytory => _selectedTerritory;

        public void SetData(ITerritoryData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _conqueredTerritories = new(data.ConqueredTerritories);
            _selectedTerritory = data.SelectedTerrytory;
        }

        public bool IsChangedFrom(ITerritoryData other)
        {
            if (other == null)
                return true;

            IReadOnlyList<int> current = ConqueredTerritories;
            IReadOnlyList<int> newList = other.ConqueredTerritories;

            if (current.Count != newList.Count)
                return true;

            for (int i = 0; i < current.Count; i++)
            {
                if (current[i] != newList[i])
                    return true;
            }

            if (_selectedTerritory != other.SelectedTerrytory)
                return true;

            return false;
        }
    }
}