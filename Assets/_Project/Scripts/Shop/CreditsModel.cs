using System;
using BattleBase.SaveService;

namespace BattleBase.ShopSystem
{
    public class CreditsModel : ISaveable
    {
        private readonly IShopSaver _saver;

        public CreditsModel(IShopSaver saver)
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
                Value -= value;

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
            Value = _saver.ShopData.Credits;

            Changed?.Invoke();
        }

        public void Save()
        {
            ShopData data = new(Value);
            _saver.SetShopData(data);
        }
    }
}