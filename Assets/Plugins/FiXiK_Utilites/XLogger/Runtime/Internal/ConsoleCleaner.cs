#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

namespace FiXiK.CustomLogger.Editor
{
    public static class ConsoleCleaner
    {
        public static void Clear()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            System.Type type = assembly.GetType(LoggerConstants.LogEntriesTypeName);
            MethodInfo method = type.GetMethod("Clear");
            method?.Invoke(new object(), null);
        }
    }
}
#endif