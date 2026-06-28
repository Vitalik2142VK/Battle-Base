using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.SaveService
{
    [Serializable]
    public class ShopData : IShopData
    {
        [SerializeField] private int _credits = 1000;
        [SerializeField] private List<UnitUpgradeData> _unitUpgrades = new();

        public ShopData() { }

        public ShopData(IShopData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _credits = data.Credits;

            _unitUpgrades.Clear();

            foreach (IUnitUpgradeData item in data.UnitsUpgrades)
                _unitUpgrades.Add(new(item));
        }

        public int Credits => _credits;

        public IReadOnlyList<IUnitUpgradeData> UnitsUpgrades => _unitUpgrades;

        public void SetData(IShopData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _credits = data.Credits;

            SetUnitsUpgrades(data.UnitsUpgrades);
        }

        public void SetUnitsUpgrades(IReadOnlyList<IUnitUpgradeData> datas)
        {
            _unitUpgrades.Clear();

            foreach (IUnitUpgradeData unitUPgrade in datas)
                _unitUpgrades.Add(new(unitUPgrade));
        }

        public void SetCredits(int value) =>
            _credits = value;

        public bool IsChangedFrom(IShopData other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_credits != other.Credits)
                return true;

            if (_unitUpgrades.Count != other.UnitsUpgrades.Count)
                return true;

            for (int i = 0; i < _unitUpgrades.Count; i++)
            {
                if (_unitUpgrades[i].IsChangedFrom(other.UnitsUpgrades[i]))
                    return true;
            }

            return false;
        }
    }
}