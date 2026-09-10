using System;
using UnityEngine;

namespace FiXiK.CustomLogger.Editor
{
    public static class LoggerTest
    {
        public static void TestLogs()
        {
            ILanguage lang = LanguageManager.Current;
            XLogger.ClearConsole();

            Debug.Log($"<b><color=white>{lang.TestTitle}</color></b>");

            // ----- Standard Logs -----            
            Debug.Log($"<b><color=white>{lang.StandardLogsTitle}</color></b>");
            XLogger.Log(lang.TestNormal);
            XLogger.LogWarning(lang.TestWarning);
            XLogger.LogError(lang.TestError);

            // ----- Tags -----
            Debug.Log($"<b><color=white>{lang.TagsTitle}</color></b>");

            foreach (TagType tag in Enum.GetValues(typeof(TagType)))
                XLogger.Log($"Test log for tag: {nameof(TagType)}.{tag}", tag);

            // ----- Colors -----
            Debug.Log($"<b><color=white>{lang.ColorsTitle}</color></b>");
            ColorParams[] colors = SettingsProvider.Settings.GetAllColors();

            foreach (ColorParams colorParam in colors)
            {
                if (colorParam == null || string.IsNullOrWhiteSpace(colorParam.Name))
                    continue;

                XLogger.Message($"Color: {nameof(LogColor)}.{colorParam.Name}")
                    .WithColor(colorParam.Color)
                    .Log();
            }

            Debug.Log($"<b><color=white>{lang.TestComplete}</color></b>");
        }
    }
}