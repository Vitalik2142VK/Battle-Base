using BattleBase.Gameplay.Actors.Production.Improve;
using BattleBase.Gameplay.Actors.Production.Spawn;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class SpawnerImprover : ISpawnerImprover
    {
        private readonly ISpawnerDataController _spawnerData;
        private readonly List<ISpawnProductionDataByTier> _availableProductionData;
        private readonly List<ISpawnProductionData> _currentProductionData;
        private readonly IImprover _improver;

        private int _currentTier;
        private int _maxTier;

        public SpawnerImprover(ISpawnerDataController spawnerData, IImprover improvement)
        {
            _spawnerData = spawnerData ?? throw new ArgumentNullException(nameof(spawnerData));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));

            _availableProductionData = new List<ISpawnProductionDataByTier>();
            _currentProductionData = new List<ISpawnProductionData>();
            _currentTier = 0;
        }

        public Type KeyType => typeof(ISpawnerImprover);

        public IEnumerable<ISpawnProductionData> SpawnDatas => _currentProductionData;

        public IImproveProductionData Data => _improver.Data;

        public bool CanImprove => _currentTier < _maxTier && _improver.CanImprove;

        public void Enable()
        {
            _currentTier = 1;
            _maxTier = 0;
            _availableProductionData.AddRange(_spawnerData.SpawnProductionDatas);

            if (_availableProductionData.Count == 0)
                return;

            CalculateMaxTier();

            if (TryGetProductionData(out ISpawnProductionData productionData))
                _currentProductionData.Add(productionData);

            _improver.Enable();
        }

        public void Disable()
        {
            _availableProductionData.Clear();
            _currentProductionData.Clear();
            _improver.Disable();
        }

        public bool TryImprove()
        {
            if (CanImprove == false)
                return false;

            if (_improver.TryImprove())
            {
                _currentTier++;

                if (TryGetProductionData(out ISpawnProductionData productionData))
                    _currentProductionData.Add(productionData);

                return true;
            }

            return false;
        }

        private void CalculateMaxTier()
        {
            foreach (var productionDataWithTier in _availableProductionData)
            {
                if (_maxTier < productionDataWithTier.Tier)
                    _maxTier = productionDataWithTier.Tier;
            }
        }

        private bool TryGetProductionData(out ISpawnProductionData productionData)
        {
            foreach (var productionDataWithTier in _availableProductionData)
            {
                if (productionDataWithTier.Tier == _currentTier)
                {
                    productionData = productionDataWithTier;

                    return true;
                }
            }

            productionData = null;

            return false;
        }
    }
}