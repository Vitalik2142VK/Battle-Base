using System;
using System.Collections.Generic;
using System.Numerics;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Core.Formatting.CompactNumber
{
    [CreateAssetMenu(
        fileName = nameof(CompactNumberFormatConfig),
        menuName = AssetMenuPaths.ScriptableObjects + "Formatting/" + nameof(CompactNumberFormatConfig))]
    public class CompactNumberFormatConfig : ScriptableObject
    {
        [SerializeField]
        private SuffixEntry[] _suffixes = DefaultSuffixes;

        [SerializeField]
        [Range(CompactNumberConstants.MinimumPrecision, CompactNumberConstants.MaximumPrecision)]
        private int _precision = 1;

        private BigInteger[] _divisors;

        public static SuffixEntry[] DefaultSuffixes => new SuffixEntry[]
        {
            new() { Exponent = 0,  Suffix = ""    },
            new() { Exponent = 3,  Suffix = "K"   },
            new() { Exponent = 6,  Suffix = "M"   },
            new() { Exponent = 9,  Suffix = "B"   },
            new() { Exponent = 12, Suffix = "T"   },
            new() { Exponent = 15, Suffix = "Qa"  },
            new() { Exponent = 18, Suffix = "Qi"  },
            new() { Exponent = 21, Suffix = "Sx"  },
            new() { Exponent = 24, Suffix = "Sp"  },
            new() { Exponent = 27, Suffix = "Oc"  },
            new() { Exponent = 30, Suffix = "No"  },
            new() { Exponent = 33, Suffix = "Dc"  },
            new() { Exponent = 36, Suffix = "Ud"  },
            new() { Exponent = 39, Suffix = "Dd"  },
            new() { Exponent = 42, Suffix = "Td"  },
            new() { Exponent = 45, Suffix = "Qad" },
            new() { Exponent = 48, Suffix = "Qid" },
            new() { Exponent = 51, Suffix = "Sxd" },
            new() { Exponent = 54, Suffix = "Spd" },
            new() { Exponent = 57, Suffix = "Ocd" },
            new() { Exponent = 60, Suffix = "Nod" },
            new() { Exponent = 63, Suffix = "Vg"  },
            new() { Exponent = 66, Suffix = "Uvg" },
            new() { Exponent = 69, Suffix = "Dvg" },
            new() { Exponent = 72, Suffix = "Tvg" },
            new() { Exponent = 75, Suffix = "Od"  },
        };

        public IReadOnlyList<SuffixEntry> Suffixes => _suffixes ?? Array.Empty<SuffixEntry>();

        public int Precision => _precision;

        public IReadOnlyList<BigInteger> Divisors
        {
            get
            {
                EnsureDivisorsCached();

                return _divisors;
            }
        }

        public void ResetToDefaults()
        {
            _suffixes = DefaultSuffixes;
            _precision = 1;
            _divisors = null;
        }

        private void EnsureDivisorsCached()
        {
            SuffixEntry[] suffixes = _suffixes ?? Array.Empty<SuffixEntry>();

            if (_divisors != null && _divisors.Length == suffixes.Length)
                return;

            _divisors = new BigInteger[suffixes.Length];

            for (int index = 0; index < suffixes.Length; index++)
                _divisors[index] = BigInteger.Pow(10, suffixes[index].Exponent);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _divisors = null;

            if (_suffixes == null || _suffixes.Length == 0)
                return;

            int previousExponent = CompactNumberConstants.MinimumExponent - 1;

            for (int index = 0; index < _suffixes.Length; index++)
            {
                SuffixEntry entry = _suffixes[index];

                if (IsUnfilled(entry))
                    continue;

                ValidateEntry(entry, previousExponent, index);
                previousExponent = entry.Exponent;
            }
        }

        private static bool IsUnfilled(SuffixEntry entry) =>
            entry.Suffix == null;

        private void ValidateEntry(SuffixEntry entry, int previousExponent, int index)
        {
            if (entry.Exponent < CompactNumberConstants.MinimumExponent)
                Debug.LogWarning($"{nameof(CompactNumberFormatConfig)}: exponent {entry.Exponent} at index {index} is below {CompactNumberConstants.MinimumExponent}", this);

            if (entry.Exponent <= previousExponent)
                Debug.LogWarning($"{nameof(CompactNumberFormatConfig)}: suffixes must be ascending by exponent (index {index})", this);
        }
#endif
    }
}