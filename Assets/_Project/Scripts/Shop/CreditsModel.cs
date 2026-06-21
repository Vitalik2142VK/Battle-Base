using System;
using BattleBase.SaveService;

namespace BattleBase.Shop
{
    public class CreditsModel : ISaveable
    {
        private readonly ICreditsSaver _saver;

        public CreditsModel(ICreditsSaver saver)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

            Load();
        }

        public event Action Changed;

        public int Value { get; private set; }

        public bool CanSpend(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be positive");

            return value <= Value;
        }

        public bool TrySpend(int value)
        {
            if (CanSpend(value))
            {
                Value -= Value;

                Changed?.Invoke();

                return true;
            }

            return false;
        }

        public void Increase(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be positive");

            Value += value;

            Changed?.Invoke();
        }

        public void Load()
        {
            Value = _saver.CreditsData.Credits;

            Changed?.Invoke();
        }

        public void Save()
        {
            CreditsData data = new(Value);
            _saver.SetCreditsData(data);
        }
    }
}