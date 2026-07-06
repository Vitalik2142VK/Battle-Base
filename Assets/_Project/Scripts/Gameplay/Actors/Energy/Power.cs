using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class Power : IPowerData
    {
        private readonly IPowerConfig _powerConfig;

        private int _currentCapacity;

        public event Action DataChanged;

        public Power(IPowerConfig powerConfig)
        {
            _powerConfig = powerConfig ?? throw new ArgumentNullException(nameof(powerConfig));
            _currentCapacity = 0;

            UsedEnergy = 0;
        }

        public int CurrentCapacity { get; private set; }

        public int UsedEnergy { get; private set; }

        public void AddCapacity(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _currentCapacity += capacity;

            if (CurrentCapacity == _powerConfig.MaxCapacity)
                return;

            CurrentCapacity = _currentCapacity;

            if (_currentCapacity > _powerConfig.MaxCapacity)
                CurrentCapacity = _powerConfig.MaxCapacity;

            DataChanged?.Invoke();
        }

        public void ReduceCapacity(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _currentCapacity -= capacity;

            if (_currentCapacity < 0)
                _currentCapacity = 0;

            if (_currentCapacity < _powerConfig.MaxCapacity)
                CurrentCapacity = _currentCapacity;
            else
                CurrentCapacity = _powerConfig.MaxCapacity;
        }

        public bool TryReserve(int power)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException(nameof(power));

            int resultPower = UsedEnergy + power;

            if (CurrentCapacity < resultPower)
                return false;

            UsedEnergy += power;

            DataChanged?.Invoke();

            return true;

        }

        public void Release(int power)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException(nameof(power));

            if (UsedEnergy < power)
                UsedEnergy = 0;
            else
                UsedEnergy -= power;

            DataChanged?.Invoke();
        }
    }
}
