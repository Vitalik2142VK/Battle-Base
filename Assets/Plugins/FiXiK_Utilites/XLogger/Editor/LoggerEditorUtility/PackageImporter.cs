using UnityEditor;
using UnityEngine;

namespace FiXiK.CustomLogger.Editor
{
    public static class PackageImporter
    {
        private const string BasePrefsKey = "FiXiK.CustomLogger_Imported";

        //[MenuItem("Tools/XLogger/" + nameof(RemoveWelcomeKey))]
        public static void RemoveWelcomeKey() =>
            EditorPrefs.DeleteKey(GetProjectKey());

        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            string projectKey = GetProjectKey();

            if (EditorPrefs.GetBool(projectKey, false))
                return;

            EditorApplication.update += ShowWelcomeOnce;
        }

        private static string GetProjectKey()
        {
            int projectHash = Application.dataPath.GetHashCode();

            return $"{BasePrefsKey}_{projectHash}";
        }

        private static void ShowWelcomeOnce()
        {
            EditorApplication.update -= ShowWelcomeOnce;

            string projectKey = GetProjectKey();

            if (EditorPrefs.GetBool(projectKey, false))
                return;

            ConsoleStripHelper.EnableStripLoggingCallstack();
            LoggerSettingsWindow.ShowWindow();
            LoggerSettingsWindow.ShowWelcomeDialog();

            EditorPrefs.SetBool(projectKey, true);
        }
    }
}