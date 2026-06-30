using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class Demolition : IDemolition
    {
        private readonly IDemolitionData _data;

        private PriceCounter _priceCounter;

        public event Action Destroyed;

        public Demolition(IDemolitionData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IDemolitionData Data => _priceCounter;

        public void Init(IProductionData currentData)
        {
            _priceCounter = new PriceCounter(_data, currentData);
        }

        public void Demolish()
        {
            Destroyed?.Invoke();
        }

        public void Enable() { }

        public void Disable() { }
    }
}