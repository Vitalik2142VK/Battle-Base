using BattleBase.Gameplay.Actors.Production;
using BattleBase.Localization;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ModifiedImproverData : IProductionData
    {
        private readonly IProductionData _data;

        public ModifiedImproverData(IProductionData data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public Sprite Icon => _data.Icon;

        public ILanguageTextsSet Name => _data.Name;

        public ILanguageTextsSet Description => _data.Description;

        public float ConstructionTime => _data.ConstructionTime;

        public bool IsSummable => _data.IsSummable;

        public int Price { get; private set; }


        public void SetPrice(int price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Price = price;
        }
    }
}