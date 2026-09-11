#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class TagTypeGenerator
    {
        private const string LoggerTagName = "LOGGER";

        /// <summary>
        /// Генерирует файл TagType.cs на основе переданных имён тегов.
        /// LOGGER всегда добавляется первым (значение 0) и не должен дублироваться.
        /// </summary>
        /// <param name="tagNames">Имена тегов из настроек (включая дефолтный, если есть).</param>
        public static void GenerateTagTypeEnum(string[] tagNames)
        {
            tagNames ??= System.Array.Empty<string>();

            HashSet<string> uniqueNames = new();

            foreach (string name in tagNames)
            {
                string trimmed = name.Trim();

                if (string.IsNullOrWhiteSpace(trimmed) == false &&
                    string.Equals(trimmed, LoggerTagName, System.StringComparison.OrdinalIgnoreCase) == false)
                {
                    uniqueNames.Add(trimmed);
                }
            }

            List<string> finalNames = new() { LoggerTagName };
            finalNames.AddRange(uniqueNames);

            StringBuilder sb = new();
            sb.AppendLine("namespace FiXiK.CustomLogger");
            sb.AppendLine("{");
            sb.AppendLine("    public enum TagType");
            sb.AppendLine("    {");

            for (int i = 0; i < finalNames.Count; i++)
            {
                string rawName = finalNames[i];
                string safeName = System.Text.RegularExpressions.Regex.Replace(rawName, @"[^a-zA-Z0-9_]", "_");

                if (char.IsDigit(safeName[0]))
                    safeName = "_" + safeName;

                if (i == 0)
                    sb.AppendLine($"        {safeName} = 0,");
                else
                    sb.AppendLine($"        {safeName},");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            string content = sb.ToString();
            string filePath = LoggerConstants.GetTagTypeFilePath();
            string fullPath = Path.Combine(Application.dataPath, filePath.Replace("Assets/", ""));

            try
            {
                File.WriteAllText(fullPath, content);
                AssetDatabase.Refresh();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to generate {nameof(TagType)} enum: {ex.Message}");
            }
        }
    }
}
#endif