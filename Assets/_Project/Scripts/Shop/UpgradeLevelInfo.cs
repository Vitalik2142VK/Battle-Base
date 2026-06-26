using System;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class UpgradeLevelInfo : IUpgradeLevelInfo
    {
        [SerializeField] private int _price;

        public UpgradeLevelInfo(IUpgradeLevelInfo other)
        {
            _price = other.Price;
        }

        public int Price => _price;
    }
}