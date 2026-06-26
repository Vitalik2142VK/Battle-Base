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

        public string Name => _name;

        public int DamageLevel => _damageLevel;

        public int ArmorLevel => _armorLevel;

        public int BuildTimeLevel => _buildTimeLevel;

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception(name);

            _name = name;
        }

        public void SetDamageLevel(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level), level, "Value must be positive");

            _damageLevel = level;
        }

        public void SetArmorLevel(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level), level, "Value must be positive");

            _armorLevel = level;
        }

        public void SetBuildTimeLevel(int level)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level), level, "Value must be positive");

            _buildTimeLevel = level;
        }

        public bool IsChangedFrom(IUnitUpgradeData other)
        {
            if (other == null)
                return true;

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