using BattleBase.Gameplay.Actors.Production;
using BattleBase.Localization;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    public class PriceCounter : IDemolitionData
    {
        private readonly IDemolitionData _data;
        private readonly IProductionData _currentData;

        public PriceCounter(IDemolitionData data, IProductionData currentData)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _currentData = currentData ?? throw new ArgumentNullException(nameof(currentData));
        }

        public Sprite Icon => _data.Icon;

        public LanguageTextsSet Name => _data.Name;

        public LanguageTextsSet Description => _data.Description;

        public float ConstructionTime => _data.ConstructionTime;

        public float ReturnedCoefficient => _data.ReturnedCoefficient;

        public bool IsSummable => _data.IsSummable;

        public int Price => (int)(_currentData.Price * _data.ReturnedCoefficient);
    }
}