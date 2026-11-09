using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class UpgradeButtonInfo : IUpgradeInfo
    {
        [Header("PriceSettings")]
        [SerializeField] private float _growthRate = 1.1f;
        [SerializeField] private float _decay = 0.3f;

        [SerializeField] private List<int> _prices = new();
        [SerializeField][HideInInspector] private int _currentLevel;

        public UpgradeButtonInfo(IUpgradeInfo other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            _prices = new(other.Levels);
            _currentLevel = other.CurrentLevel;
        }

        public float GrowthRate => _growthRate;

        public float Decay => _decay;

        public int MaximumLevel => _prices.Count - 1;

        public int CurrentLevel => _currentLevel;

        public IReadOnlyList<int> Levels => _prices;

        public int CurrentPrice => _prices[CurrentLevel];

        public void Increase() =>
            _currentLevel++;

        public void SetLevel(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level), level, "Value must be positive");

            _currentLevel = level;
        }

        public void SetPrice(int index, int price)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            if (index < 0 || index >= _prices.Count)
                throw new ArgumentOutOfRangeException(nameof(index));

            _prices[index] = price;
        }
    }
}