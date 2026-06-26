using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class UpgradeButtonInfo : IUpgradeButtonInfo
    {
        [SerializeField] private List<UpgradeLevelInfo> _levels = new();
        [SerializeField] private int _currentLevel;

        public UpgradeButtonInfo(IUpgradeButtonInfo other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            _levels.Clear();

            foreach (IUpgradeLevelInfo info in other.Levels)
                _levels.Add(new(info));

            _currentLevel = other.CurrentLevel;
        }

        public int MaximumLevel => _levels.Count - 1;

        public int CurrentLevel => _currentLevel;

        public IReadOnlyList<IUpgradeLevelInfo> Levels => _levels;

        public int CurrentPrice => _levels[CurrentLevel].Price;

        public void Increase() =>
            _currentLevel++;
    }
}