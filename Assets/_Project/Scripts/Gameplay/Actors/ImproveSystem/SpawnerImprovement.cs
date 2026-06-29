using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class SpawnerImprovement : ISpawnerImprovement
    {
        private readonly List<IActorData> _availableActorDatas;
        private readonly List<IActorData> _currentActorDatas;
        private readonly IImprovement _improvement;

        private int _currentNumImprove;

        public SpawnerImprovement(IActorDataStorage actorStorage, IImprovement improvement)
        {
            if (actorStorage == null)
                throw new ArgumentNullException(nameof(actorStorage));

            _availableActorDatas = new List<IActorData>(actorStorage.ActorDatas);
            _currentActorDatas = new List<IActorData>();
            _improvement = improvement ?? throw new ArgumentNullException(nameof(improvement));
            _currentNumImprove = 0;
        }

        public IEnumerable<IActorData> ActorDatas => _currentActorDatas;

        public IImprovementData Data => _improvement.Data;

        public bool CanImprove => _currentNumImprove < _availableActorDatas.Count;

        public void Init(IProductionData currentData) =>
            _improvement.Init(currentData);

        public void Enable()
        {
            _currentNumImprove = 0;
            _improvement.Enable();

            Improve();
        }

        public void Disable()
        {
            _currentActorDatas.Clear();
            _improvement.Disable();
        }

        public void Improve()
        {
            if (CanImprove == false)
                return;

            _currentActorDatas.Add(_availableActorDatas[_currentNumImprove++]);
            _improvement.Improve();
        }
    }
}