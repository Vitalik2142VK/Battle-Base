using System;
using UnityEngine;

namespace BattleBase.SaveService
{
    [Serializable]
    public class CreditsData : ICreditsData
    {
        [SerializeField] private int _credits = 1000;

        public CreditsData() { }

        public CreditsData(int credits)
        {
            if (credits < 0)
                throw new ArgumentOutOfRangeException(nameof(credits), credits, "Value must be positive");

            _credits = credits;
        }

        public int Credits => _credits;

        public void SetData(ICreditsData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            _credits = data.Credits;
        }

        public bool IsChangedFrom(ICreditsData other)
        {
            if (other == null)
                return true;

            if (_credits != other.Credits)
                return true;

            return false;
        }
    }
}