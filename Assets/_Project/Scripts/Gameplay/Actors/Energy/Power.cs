using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public class Power : IPowerData
    {
        private readonly IPowerConfig _powerConfig;

        public event Action DataChanged;

        public Power(IPowerConfig powerConfig)
        {
            _powerConfig = powerConfig ?? throw new ArgumentNullException(nameof(powerConfig));
            UsedEnergy = 0;
        }

        public int CurrentCapacity { get; private set; }

        public int UsedEnergy { get; private set; }

        public void AddCapacity(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (CurrentCapacity == _powerConfig.MaxCapacity)
                return;

            CurrentCapacity = +capacity;

            if (CurrentCapacity > _powerConfig.MaxCapacity)
                CurrentCapacity = _powerConfig.MaxCapacity;

            DataChanged?.Invoke();
        }

        public bool TryReserve(int power)
        {
            if (power < 0)
                throw new ArgumentOutOfRangeException(nameof(power));

            if (CurrentCapacity > UsedEnergy)
            {
                UsedEnergy += power;

                DataChanged?.Invoke();

                return true;
            }

            return false;
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
