using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;

namespace BattleBase.Core.Formatting.CompactNumber
{
    public static class CompactNumberFormatter
    {
        private const string ZeroFormattedValue = "0";
        private const string NegativePrefix = "-";
        private const char DecimalSeparator = '.';
        private const char ZeroDigit = '0';
        private const string InvalidDoubleMessage = "Value cannot be NaN or infinity";
        private const string NotInitializedMessageTemplate = "{0} is not initialized. Call Initialize first.";
        private static readonly string[] _fixedPointFormats = { "F0", "F1", "F2", "F3", "F4" };

        private static CompactNumberFormatConfig _config;

        public static bool IsInitialized => _config != null;

        public static void Initialize(CompactNumberFormatConfig config)
        {
            _config = config != null
                ? config
                : throw new ArgumentNullException(nameof(config));
        }

        public static string Format(BigInteger value)
        {
            if (_config == null)
                throw new InvalidOperationException(
                    string.Format(NotInitializedMessageTemplate, nameof(CompactNumberFormatter)));

            return Format(value, _config);
        }

        public static string Format(int value) =>
            Format((BigInteger)value);

        public static string Format(long value) =>
            Format((BigInteger)value);

        public static string Format(float value) =>
            Format((double)value);

        public static string Format(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException(InvalidDoubleMessage, nameof(value));

            return Format(new BigInteger(value));
        }

        public static string Format(BigInteger value, CompactNumberFormatConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (value.IsZero)
                return ZeroFormattedValue;

            bool isNegative = value.Sign < 0;
            BigInteger absoluteValue = isNegative ? -value : value;
            string formattedAbsoluteValue = FormatAbsolute(absoluteValue, config);

            return isNegative ? NegativePrefix + formattedAbsoluteValue : formattedAbsoluteValue;
        }

        public static string Format(int value, CompactNumberFormatConfig config) =>
            Format((BigInteger)value, config);

        public static string Format(long value, CompactNumberFormatConfig config) =>
            Format((BigInteger)value, config);

        public static string Format(float value, CompactNumberFormatConfig config) =>
            Format((double)value, config);

        public static string Format(double value, CompactNumberFormatConfig config)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException(InvalidDoubleMessage, nameof(value));

            return Format(new BigInteger(value), config);
        }

        private static string FormatAbsolute(BigInteger value, CompactNumberFormatConfig config)
        {
            IReadOnlyList<SuffixEntry> suffixes = config.Suffixes;
            IReadOnlyList<BigInteger> divisors = config.Divisors;

            if (suffixes.Count == 0)
                return value.ToString(CultureInfo.InvariantCulture);

            for (int index = suffixes.Count - 1; index >= 0; index--)
            {
                if (value < divisors[index])
                    continue;

                double scaledValue = ComputeScaledValue(value, divisors[index]);

                if (double.IsInfinity(scaledValue))
                    return value.ToString(CultureInfo.InvariantCulture) + suffixes[index].Suffix;

                return FormatScaled(scaledValue, config.Precision) + suffixes[index].Suffix;
            }

            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static double ComputeScaledValue(BigInteger value, BigInteger divisor)
        {
            BigInteger integerPart = value / divisor;
            BigInteger remainder = value % divisor;
            double fractionPart = (double)remainder / (double)divisor;

            return (double)integerPart + fractionPart;
        }

        private static string FormatScaled(double value, int precision)
        {
            double truncatedValue = TruncateToPrecision(value, precision);

            string formattedValue = truncatedValue.ToString(
                _fixedPointFormats[precision],
                CultureInfo.InvariantCulture);

            return RemoveTrailingZeros(formattedValue, precision);
        }

        private static string RemoveTrailingZeros(string formattedValue, int precision)
        {
            if (precision == CompactNumberConstants.MinimumPrecision)
                return formattedValue;

            if (formattedValue.Contains(DecimalSeparator) == false)
                return formattedValue;

            return formattedValue.TrimEnd(ZeroDigit).TrimEnd(DecimalSeparator);
        }

        private static double TruncateToPrecision(double value, int precision)
        {
            if (precision == CompactNumberConstants.MinimumPrecision)
                return Math.Truncate(value);

            double multiplier = Math.Pow(10, precision);

            return Math.Truncate(value * multiplier) / multiplier;
        }
    }
}