using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class LoggerConstants
    {
        public const string ShowTagField = "_showTag";
        public const string DefaultTagField = "_defaultTag";
        public const string TagsField = "_tags";
        public const string LogTextParamsField = "_logTextParams";
        public const string WarningTextParamsField = "_warningTextParams";
        public const string ErrorTextParamsField = "_errorTextParams";
        public const string TextColorField = "_textColor";
        public const string FontStyleField = "_fontStyle";

        public const string ResourcesPath = "LoggerSettings";

        public const string DefaultTagName = "LOGGER";
        public const string LogEntriesTypeName = "UnityEditor.LogEntries";
        public const string EditorCondition = "UNITY_EDITOR";
        public const string DevelopmentCondition = "DEVELOPMENT_BUILD";

        public const FontStyle DefaultTagStyle = FontStyle.Bold;

        public const string Version = "1.0.1";
        public const string TelegramUrl = "https://t.me/VL_dogs";
        public const string MenuIconName = "_Menu";

#if UNITY_EDITOR
        public static string GetRuntimeFolder()
        {
            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:Script {nameof(LoggerConstants)}");

            if (guids.Length == 0)
                return "Assets/Plugins/FiXiK_Utilites/XLogger/Runtime";

            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);

            return System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(path)).Replace('\\', '/');
        }

        public static string GetTagTypeFilePath() =>
            $"{GetRuntimeFolder()}/Internal/Data/Types/TagType.cs";

        public static string GetLogColorFilePath() =>
            $"{GetRuntimeFolder()}/Internal/Data/Colors/LogColor.cs";

        public static string GetDefaultResourcesPath()
        {
            string runtime = GetRuntimeFolder();
            string xlogger = System.IO.Path.GetDirectoryName(runtime);

            return $"{xlogger}/Resources".Replace('\\', '/');
        }

        public static string GetParentFolderPath()
        {
            string runtime = GetRuntimeFolder();

            return System.IO.Path.GetDirectoryName(runtime).Replace('\\', '/');
        }

        public static string GetGrandParentFolderPath()
        {
            string parent = GetParentFolderPath();

            return System.IO.Path.GetDirectoryName(parent).Replace('\\', '/');
        }
#endif
    }
}