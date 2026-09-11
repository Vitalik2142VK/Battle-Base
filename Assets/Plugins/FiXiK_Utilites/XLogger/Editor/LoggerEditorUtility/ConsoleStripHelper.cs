#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FiXiK.CustomLogger.Editor
{
    public static class ConsoleStripHelper
    {
        public static void EnableStripLoggingCallstack(bool enable = true)
        {
            try
            {
                Type logEntriesType = typeof(EditorWindow).Assembly.GetType("UnityEditor.LogEntries");
                if (logEntriesType == null)
                    return;

                Type consoleFlagsType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow+ConsoleFlags")
                    ?? typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow")
                        ?.GetNestedType("ConsoleFlags", BindingFlags.NonPublic);

                if (consoleFlagsType == null)
                    return;

                object stripFlag = Enum.Parse(consoleFlagsType, "StripLoggingCallstack");
                int flagValue = (int)stripFlag;

                MethodInfo setFlagMethod = logEntriesType.GetMethod(
                    "SetConsoleFlag",
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                setFlagMethod?.Invoke(null, new object[] { flagValue, enable });
            }
            catch 
            {
                Debug.Log(
                    "<b><color=#FFEB3B>[XLogger]</color></b> " +
                    "Failed to automatically enable <b>Strip logging callstack</b>.\n" +
                    "Open the Console → click the ⋮ (three dots in the top-right) → enable <b>Strip logging callstack</b> manually.\n" +
                    "This will keep the log stack traces cleaner.");
            }
        }
    }
}
#endif