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
        private readonly IMaterialRegistry _materialRegistry;
        private readonly ITeamable _teamable;

        private int _currentNumImprove;

        public SpawnerImprover(
            IActorDataStorage actorStorage, 
            IImprover improvement, 
            IMaterialRegistry materialRegistry,
            ITeamable teamable)
        {
            if (actorStorage == null)
                throw new ArgumentNullException(nameof(actorStorage));

            _availableActorDatas = new List<IActorData>(actorStorage.ActorDatas);
            _currentActorDatas = new List<IActorData>();
            _improver = improvement ?? throw new ArgumentNullException(nameof(improvement));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
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

        public void Improve()
        {
            if (CanImprove == false)
                return;

            if (_materialRegistry.TrySpend(_teamable.TeamType, _improver.Data.Price) == false)
                return;

            _currentActorDatas.Add(_availableActorDatas[_currentNumImprove++]);
            _improver.Improve();
        }
    }
}