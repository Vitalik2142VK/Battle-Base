using BattleBase.Localization;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ModifiedPriceImproverData : IImproverData
    {
        private readonly IImproverData _data;

        private int _initialPrice;

        public ModifiedPriceImproverData(IImproverData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public Sprite Icon => _data.Icon;

        public LanguageTextsSet Name => _data.Name;

        public LanguageTextsSet Description => _data.Description;

        public float ConstructionTime => _data.ConstructionTime;

        public float PriceCoefficient => _data.PriceCoefficient;

        public bool IsSummable => _data.IsSummable;

        public int Price { get; private set; }

        public void Reset()
        {
            Price = _initialPrice;
        }

        public void Modify()
        {
            Price = (int)((Price + _data.Price) * _data.PriceCoefficient);
        }

        public void SetInitialPrice(int initialPrice)
        {
            if (initialPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(initialPrice));

            _initialPrice = initialPrice;

            Reset();
        }
    }
}