using BattleBase.SaveService;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Map
{
    public class TerritoriesModel : ISaveable, ISelectedTerritory
    {
        private readonly ITerritorySaver _saver;
        private readonly IReadOnlyList<TerritoryConfig> _territoryConfigs;

        private TerritoriesData _territoryData = new();

        public TerritoriesModel(IReadOnlyList<TerritoryConfig> territoryConfigs, ITerritorySaver saver)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _territoryConfigs = territoryConfigs ?? throw new ArgumentNullException(nameof(territoryConfigs));

            Load();
        }

        public event Action Changed;

        public ITerritoryInfo SelectedTerritoryInfo => GetTerritoryInfo(SelectedTerritoryIndex);

        public int SelectedTerritoryIndex => _territoryData.SelectedTerritoryIndex;

        public IReadOnlyList<int> ConqueredTerritoryIndices => _territoryData.ConqueredTerritoryIndices;

        private int TerritoriesCount => _territoryConfigs.Count;

        public void SetSelectedTerritoryIndex(int index)
        {
            if (index < 0 || index >= TerritoriesCount)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (SelectedTerritoryIndex == index)
                return;

            _territoryData.SetSelectedTerritoryIndex(index);

            Changed?.Invoke();
        }

        public ITerritoryInfo GetTerritoryInfo(int index)
        {
            if (index < 0 || index >= TerritoriesCount)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"Index is out of range. Max index = {_territoryConfigs.Count}");

            return _territoryConfigs[index];
        }

        public bool TryMarkTerritoryConquered(int index)
        {
            if (index < 0 || index >= TerritoriesCount)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_territoryData.TryMarkTerritoryConquered(index))
            {
                Changed?.Invoke();

                return true;
            }

            return false;
        }

        public int GetCreditsForFirstVictory(int index) =>
            GetTerritoryInfo(index).CreditsForFirstVictory;

        public bool TryGetFirstUnconqueredTerritoryIndex(out int index)
        {
            for (int i = 0; i < TerritoriesCount; i++)
            {
                if (_territoryData.IsTerritoryConquered(i))
                    continue;

                index = i;

                return true;
            }

            index = -1;

            return false;
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