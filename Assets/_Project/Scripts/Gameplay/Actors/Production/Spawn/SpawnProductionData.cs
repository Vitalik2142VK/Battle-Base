using BattleBase.Utils.Constants;
using System;

namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public class SpawnProductionData : ISpawnProductionDataByTier
    {
        private readonly IActorData _data;

        private float _timeSpent;
        private int _count;

        public event Action DataChanged;

        public SpawnProductionData(IActorData data, int tier)
        {
            if (tier < 0)
                throw new ArgumentOutOfRangeException(nameof(tier));

            _data = data ?? throw new ArgumentNullException(nameof(data));
            _timeSpent = 0;

            Tier = tier;
            _count = 0;
        }

        public IActorData ActorData => _data;

        public float ConstructionProgress { get; private set; }

        public int Tier { get; }

        public int Count => _count;

        public bool IsInProcessSpawn => _timeSpent != 0;

        public void Disable()
        {
            _count = 0;
            ResetTimeSpent();
        }

        public void IncreaseCount()
        {
            if (_count < Values.MaxCountSpawnProductionData)
                _count++;
        }

        public void ReduceCount()
        {
            _count--;

            if (_count < 0)
                _count = 0;
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