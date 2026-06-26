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

        public ShopData(int credits)
        {
            if (credits < 0)
                throw new ArgumentOutOfRangeException(nameof(credits), credits, "Value must be positive");

            _credits = credits;
        }

        public int Credits => _credits;

        public IReadOnlyList<IUnitUpgradeData> UnitsUpgrades => _unitUpgrades;

        public void SetData(IShopData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _credits = data.Credits;

            _unitUpgrades.Clear();

            foreach (IUnitUpgradeData unitUpgrade in data.UnitsUpgrades)
            {
                UnitUpgradeData unitData = new();

                unitData.SetName(unitUpgrade.Name);
                unitData.SetDamageLevel(unitUpgrade.DamageLevel);
                unitData.SetArmorLevel(unitUpgrade.ArmorLevel);
                unitData.SetBuildTimeLevel(unitUpgrade.BuildTimeLevel);

                _unitUpgrades.Add(unitData);
            }
        }

        public bool IsChangedFrom(IShopData other)
        {
            if (other == null)
                return true;

            if (_credits != other.Credits)
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