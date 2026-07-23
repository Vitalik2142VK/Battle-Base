using System;

namespace BattleBase.Utils
{
    public static class MaskUtility
    {
        public static bool Contains<T>(T mask, T value) where T : Enum
        {
            long maskValue = Convert.ToInt64(mask);
            long targetValue = Convert.ToInt64(value);

            return (maskValue & targetValue) == targetValue;
        }

        public static bool ContainsAny<T>(T mask, T value) where T : Enum
        {
            long maskValue = Convert.ToInt64(mask);
            long targetValue = Convert.ToInt64(value);

            return (maskValue & targetValue) != 0;
        }

        public static bool ContainsOnlyOneFlag<T>(T value) where T : Enum
        {
            long flag = Convert.ToInt64(value);

            return flag != 0 && (flag & (flag - 1)) == 0;
        }
    }
}