using System;

namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public class SpawnProductionData : ISpawnProductionData
    {
        private readonly IActorData _data;

        private float _timeSpent;

        public event Action DataChanged;

        public SpawnProductionData(IActorData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _timeSpent = _data.ConstructionTime;
            Count = 0;
        }

        public IActorData ActorData => _data;

        public float ConstructionProgress { get; private set; }

        public int Count { get; private set; }

        public void IncreaseCount() =>
            Count++;

        public void ReduceCount()
        {
            Count--;

            if (Count < 0)
                Count = 0;
        }

        public void CalculateProcess(float delta)
        {
            if (delta < 0)
                throw new ArgumentOutOfRangeException(nameof(delta));

            _timeSpent += delta;

            if (_timeSpent >= _data.ConstructionTime)
                _timeSpent = _data.ConstructionTime;
            else
                UpdateData();

            ConstructionProgress = 1f - _timeSpent / _data.ConstructionTime;
        }

        public void ResetTimeSpent()
        {
            _timeSpent = 0;
            ConstructionProgress = 0;
        }       

        public void UpdateData() =>
            DataChanged?.Invoke();
    }
}