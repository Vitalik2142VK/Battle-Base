using System;

namespace BattleBase.Utils
{
    public static class TypeTools
    {
        public static Type FindDerivedInterface<T>(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            return FindDerivedInterface<T>(obj.GetType());
        }

        public static Type FindDerivedInterface<T>(Type type)
        {
            Type baseType = typeof(T);

            foreach (Type interfaceType in type.GetInterfaces())
            {
                if (interfaceType == baseType)
                    continue;

                if (baseType.IsAssignableFrom(interfaceType))
                    return interfaceType;
            }

            return baseType;
        }
    }
}