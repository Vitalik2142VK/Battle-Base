using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class TextFormatter
    {
        public static string WrapSquareBrackets(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return $"[{text}]";
        }

        public static string WithColor(this string text, Color32 color)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string hex = ColorUtility.ToHtmlStringRGB(color);

            return $"<color=#{hex}>{text}</color>";
        }

        public static string WithStyle(this string text, FontStyle style)
        {
            return style switch
            {
                FontStyle.Normal => text,
                FontStyle.Bold => $"<b>{text}</b>",
                FontStyle.Italic => $"<i>{text}</i>",
                FontStyle.BoldAndItalic => $"<b><i>{text}</i></b>",
                _ => text,
            };
        }

        /// <summary>
        /// Форматирует сообщение с заданным цветом и стилем.
        /// </summary>
        public static string FormatMessage(object message, Color32 color, FontStyle style)
        {
            string text = message != null ? message.ToString() : "null";

            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
                lines[i] = lines[i].WithColor(color).WithStyle(style);

            return string.Join("\n", lines);
        }
    }
}