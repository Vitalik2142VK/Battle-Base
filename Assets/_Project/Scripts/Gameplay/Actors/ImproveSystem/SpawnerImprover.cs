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
        private readonly List<ISpawnProductionData> _availableActorDatas;
        private readonly List<ISpawnProductionData> _currentActorDatas;
        private readonly IImprover _improver;

        private int _currentNumImprove;

        public SpawnerImprover(ISpawnerDataController spawnerData, IImprover improvement)
        {
            _spawnerData = spawnerData ?? throw new ArgumentNullException(nameof(spawnerData));
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));

            _availableActorDatas = new List<ISpawnProductionData>();
            _currentActorDatas = new List<ISpawnProductionData>();
            _currentNumImprove = 0;
        }

        public Type KeyType => typeof(ISpawnerImprover);

        public IEnumerable<ISpawnProductionData> SpawnDatas => _currentActorDatas;

        public IImproveProductionData Data => _improver.Data;

        public bool CanImprove => _currentNumImprove < _availableActorDatas.Count && _improver.CanImprove;

        public void Enable()
        {
            _currentNumImprove = 0;
            _availableActorDatas.AddRange(_spawnerData.SpawnDatas);

            if (_availableActorDatas.Count == 0)
                return;

            _currentActorDatas.Add(_availableActorDatas[_currentNumImprove++]);
            _improver.Enable();
        }

        public void Disable()
        {
            _availableActorDatas.Clear();
            _currentActorDatas.Clear();
            _improver.Disable();
        }

        public bool TryImprove()
        {
            if (CanImprove == false)
                return false;

            if (_improver.TryImprove())
            {
                _currentActorDatas.Add(_availableActorDatas[_currentNumImprove++]);

                return true;
            }

            return false;
        }
    }
}