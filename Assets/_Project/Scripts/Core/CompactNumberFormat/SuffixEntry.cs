using System;
using UnityEngine;

namespace BattleBase.Core.Formatting.CompactNumber
{
    [Serializable]
    public struct SuffixEntry
    {
        [Range(CompactNumberConstants.MinimumExponent, CompactNumberConstants.MaximumExponent)]
        public int Exponent;

        public string Suffix;
    }
}