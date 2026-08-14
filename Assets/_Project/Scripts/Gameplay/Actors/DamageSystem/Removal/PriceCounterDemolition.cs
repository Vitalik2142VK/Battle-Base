using BattleBase.Gameplay.Actors.ImproveSystem;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Localization;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class PriceCounterDemolition : IPriceCounterDemolition
    {
        private readonly IDemolitionData _data;
        private readonly IProductionData _currentData;
        private readonly IImproverComponent _improverComponent;

        private int _salePrice;

        public PriceCounterDemolition(
            IDemolitionData data, 
            IProductionData currentData,
            IImproverComponent improverComponent = null)
        {
            if (currentData == null)
                throw new ArgumentNullException(nameof(currentData));

            _data = data ?? throw new ArgumentNullException(nameof(data));
            _currentData = currentData ?? throw new ArgumentNullException(nameof(currentData));
            _improverComponent = improverComponent;

            _salePrice = (int)(currentData.Price * data.ReturnedCoefficient);
        }

        public Sprite Icon => _data.Icon;

        public ILanguageTextsSet Name => _data.Name;

        public ILanguageTextsSet Description => _data.Description;

        public float ConstructionTime => _data.ConstructionTime;

        public float ReturnedCoefficient => _data.ReturnedCoefficient;

        public bool IsHidden => _data.IsHidden;

        public int Price => _salePrice;

        public void Enable()
        {
            if (_improverComponent != null)
                _improverComponent.Improved += OnCaldulatedPrice;

            _salePrice = (int)(_currentData.Price * _data.ReturnedCoefficient);
        }

        public void Disable()
        {
            if (_improverComponent != null)
                _improverComponent.Improved -= OnCaldulatedPrice;
        }

        private void OnCaldulatedPrice()
        {
            int improvementPrice = _improverComponent.Data.Price;

            _salePrice += (int)(improvementPrice * _data.ReturnedCoefficient);
        }
    }
}