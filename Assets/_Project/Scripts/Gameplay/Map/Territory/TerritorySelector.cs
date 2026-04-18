using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TerritorySelector : MonoBehaviour
    {
        private Territory _selectedTerritory;

        public event Action<Territory> Unselected;
        public event Action<Territory> Selected;

        public void Select(Territory territory)
        {
            if (_selectedTerritory == territory)
                return;

            Unselect();
            _selectedTerritory = territory;

            Debug.Log("Выбрана территоия: " + territory.gameObject.name);
            Selected?.Invoke(territory);
        }

        public void Unselect()
        {
            if (_selectedTerritory == null)
                return;

            Debug.Log("Территоия: " + _selectedTerritory.gameObject.name + " больше не выбрана");
            Unselected?.Invoke(_selectedTerritory);

            _selectedTerritory = null;
        }
    }
}