using BattleBase.SaveService;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Map
{
    public class TerritoriesModel : ISaveable, ISelectedTerritory
    {
        private readonly ITerritorySaver _saver;
        private readonly IReadOnlyList<TerritoryConfig> _territoryConfigs;

        private TerritoryData _territoryData = new();

        public TerritoriesModel(IReadOnlyList<TerritoryConfig> territoryConfigs, ITerritorySaver saver)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _territoryConfigs = territoryConfigs ?? throw new ArgumentNullException(nameof(territoryConfigs));

            Load();
        }

        public event Action Changed;

        public ITerritoryInfo SelectedInfo => GetTerritoryInfo(Selected);

        public int Selected => _territoryData.SelectedTerritory;

        public IReadOnlyList<int> ConqueredTerritories => _territoryData.ConqueredTerritories;

        public void SetSelectedTerritory(int index)
        {
            if (index < 0 || index >= _territoryConfigs.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _territoryData.SetSelectedTerritory(index);
        }

        public ITerritoryInfo GetTerritoryInfo(int index)
        {
            if (index < 0 || index >= _territoryConfigs.Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index is out of range. Max index = {_territoryConfigs.Count}");

            return _territoryConfigs[index];
        }

        public bool TryAddConqueredTerritory(int index)
        {
            if (index < 0 || index >= _territoryConfigs.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return _territoryData.TryAddConqueredTerritory(index);
        }

        public int GetCreditsForFirstVictory(int index)
        {
            if (index < 0 || index >= _territoryConfigs.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            return GetTerritoryInfo(index).CreditsForFirstVictory;

        }

        public void Load()
        {
            _territoryData = new(_saver.TerritoryData);

            Changed?.Invoke();
        }

        public void Save() =>
            _saver.SetTerritoryData(_territoryData);
    }
}