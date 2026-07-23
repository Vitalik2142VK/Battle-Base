using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class SpawnerImprover : ISpawnerImprover
    {
        private readonly List<IActorData> _availableActorDatas;
        private readonly List<IActorData> _currentActorDatas;
        private readonly IImprover _improver;

        private int _currentNumImprove;

        public SpawnerImprover(
            IActorDataStorage actorStorage, 
            IImprover improvement)
        {
            if (actorStorage == null)
                throw new ArgumentNullException(nameof(actorStorage));

            _availableActorDatas = new List<IActorData>(actorStorage.ActorDatas);
            _currentActorDatas = new List<IActorData>();
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
            _currentNumImprove = 0;
        }

        public IEnumerable<IActorData> ActorDatas => _currentActorDatas;

        public IProductionData Data => _improver.Data;

        public bool CanImprove => _currentNumImprove < _availableActorDatas.Count && _improver.CanImprove;

        public void Enable()
        {
            _currentNumImprove = 0;
            _currentActorDatas.Add(_availableActorDatas[_currentNumImprove++]);
            _improver.Enable();
        }

        public void Disable()
        {
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