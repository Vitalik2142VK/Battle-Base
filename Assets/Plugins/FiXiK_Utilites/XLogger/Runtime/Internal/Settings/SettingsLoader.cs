using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class SettingsLoader
    {
        public static LoggerSettings Load()
        {
            LoggerSettings settings = Resources.Load<LoggerSettings>(LoggerConstants.ResourcesPath);

            if (settings == null)
            {
#if UNITY_EDITOR
                settings = CreateSettingsAsset();
#else
                settings = ScriptableObject.CreateInstance<LoggerSettings>();
                settings.ResetToDefault();
                Debug.LogWarning("LoggerSettings asset not found in Resources. Using in-memory defaults.");
#endif
            }

            return settings;
        }

#if UNITY_EDITOR
        private static LoggerSettings CreateSettingsAsset()
        {
            string resourcesPath = LoggerConstants.GetDefaultResourcesPath();

            if (UnityEditor.AssetDatabase.IsValidFolder(resourcesPath) == false)
            {
                string parent = LoggerConstants.GetParentFolderPath();

                if (UnityEditor.AssetDatabase.IsValidFolder(parent) == false)
                {
                    string grandParent = LoggerConstants.GetGrandParentFolderPath();

                    if (UnityEditor.AssetDatabase.IsValidFolder(grandParent) == false)
                        grandParent = "Assets";

                    UnityEditor.AssetDatabase.CreateFolder(grandParent, "XLogger");
                    parent = $"{grandParent}/XLogger";
                }

                UnityEditor.AssetDatabase.CreateFolder(parent, "Resources");
            }

            LoggerSettings newSettings = ScriptableObject.CreateInstance<LoggerSettings>();
            newSettings.ResetToDefault();
            newSettings.name = LoggerConstants.ResourcesPath;

            string assetPath = $"{resourcesPath}/{LoggerConstants.ResourcesPath}.asset";
            UnityEditor.AssetDatabase.CreateAsset(newSettings, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            TagTypeGenerator.GenerateTagTypeEnum(newSettings.GetAllTagNames());
            LogColorGenerator.GenerateLogColorStruct(newSettings.GetAllColors());

            Debug.Log($"LoggerSettings asset created at {assetPath}");

            return Resources.Load<LoggerSettings>(LoggerConstants.ResourcesPath);
        }
#endif
    }
}