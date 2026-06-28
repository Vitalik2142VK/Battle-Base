using System;
using System.Collections.Generic;
using BattleBase.SaveService;

namespace BattleBase.Gameplay.Map
{
    public class TerritoriesModel : ISaveable
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
                throw new ArgumentOutOfRangeException(nameof(index));

            return _territoryConfigs[index];
        }

        public void AddConqueredTerritory(int index)
        {
            if (index < 0 || index >= _territoryConfigs.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _territoryData.AddConqueredTerritory(index);
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