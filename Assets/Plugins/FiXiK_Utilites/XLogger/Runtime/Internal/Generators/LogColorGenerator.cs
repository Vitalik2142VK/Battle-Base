#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class LogColorGenerator
    {
        public static void GenerateLogColorStruct(ColorParams[] colors)
        {
            if (colors == null || colors.Length == 0)
            {
                Debug.LogWarning("No colors to generate. Skipping.");

                return;
            }

            HashSet<string> uniqueNames = new();
            List<ColorParams> uniqueColors = new();

            foreach (ColorParams colorParam in colors)
            {
                string name = colorParam.Name.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                if (uniqueNames.Add(name.ToUpperInvariant()))
                    uniqueColors.Add(colorParam);
            }

            if (uniqueColors.Count == 0)
            {
                Debug.LogWarning("No valid unique colors to generate. Skipping.");
                return;
            }

            StringBuilder sb = new();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine();
            sb.AppendLine("namespace FiXiK.CustomLogger");
            sb.AppendLine("{");
            sb.AppendLine("    public readonly struct LogColor");
            sb.AppendLine("    {");

            foreach (ColorParams colorParam in uniqueColors)
            {
                string name = colorParam.Name.Trim();

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                string safeName = System.Text.RegularExpressions.Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");

                if (char.IsDigit(safeName[0]))
                    safeName = "_" + safeName;

                Color32 color = colorParam.Color;
                sb.AppendLine($"        public static Color32 {safeName} => new Color32({color.r}, {color.g}, {color.b}, {color.a});");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            string content = sb.ToString();
            string filePath = LoggerConstants.GetLogColorFilePath();
            string fullPath = Path.Combine(Application.dataPath, filePath.Replace("Assets/", ""));

            try
            {
                string directory = Path.GetDirectoryName(fullPath);
                if (Directory.Exists(directory) == false)
                    Directory.CreateDirectory(directory);

                File.WriteAllText(fullPath, content);
                AssetDatabase.Refresh();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to generate LogColor struct: {ex.Message}");
            }
        }
    }
}
#endif