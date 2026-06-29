using System;
using UnityEngine;

namespace BattleBase.SaveService
{
    [Serializable]
    public class UnitUpgradeData : IUnitUpgradeData
    {
        [SerializeField] private string _name;
        [SerializeField] private int _damageLevel;
        [SerializeField] private int _armorLevel;
        [SerializeField] private int _buildTimeLevel;

        public UnitUpgradeData(string name, int damageLevel, int armorLevel, int buildTimeLevel)
        {
            _name = name;
            _damageLevel = damageLevel;
            _armorLevel = armorLevel;
            _buildTimeLevel = buildTimeLevel;
        }

        public UnitUpgradeData(IUnitUpgradeData data)
        {
            _name = data.Name;
            _damageLevel = data.DamageLevel;
            _armorLevel = data.ArmorLevel;
            _buildTimeLevel = data.BuildTimeLevel;
        }

        public string Name => _name;

        public int DamageLevel => _damageLevel;

        public int ArmorLevel => _armorLevel;

        public int BuildTimeLevel => _buildTimeLevel;

        public bool IsChangedFrom(IUnitUpgradeData other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_name != other.Name)
                return true;

            if (_damageLevel != other.DamageLevel)
                return true;

            if (_armorLevel != other.ArmorLevel)
                return true;

            if (_buildTimeLevel != other.BuildTimeLevel)
                return true;

            return false;
        }
    }
}