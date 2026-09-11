using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class XLogger
    {
        private const string EditorCondition = LoggerConstants.EditorCondition;
        private const string DevelopmentCondition = LoggerConstants.DevelopmentCondition;


        [System.Diagnostics.Conditional(EditorCondition)]
        public static void ClearConsole()
        {
#if UNITY_EDITOR
            Editor.ConsoleCleaner.Clear();
#endif
        }

        // ---------- Базовые методы ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, bool condition = true) =>
            LoggerBuilder.Message(message).WithCondition(condition).Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithCondition(condition)
                .Log();
        }

        // ---------- С контекстом ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, Object context, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithContext(context)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, Object context, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithContext(context)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, Object context, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithContext(context)
                .WithCondition(condition)
                .Log();
        }

        // ---------- С цветом ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        // ---------- Со стилем шрифта ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, FontStyle fontStyle, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, FontStyle fontStyle, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, FontStyle fontStyle, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();
        }

        // ---------- С цветом и контекстом ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, Object context, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithContext(context)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, Object context, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithContext(context)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, Object context, Color32 color, bool condition = true)
        {
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithContext(context)
                .WithColor(color)
                .WithCondition(condition)
                .Log();
        }

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, Object context, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithContext(context)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, Object context, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithContext(context)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, Object context, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithContext(context)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        // ---------- С контекстом, цветом и стилем ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, Object context, Color32 color, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithContext(context)
                .WithColor(color)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, Object context, Color32 color, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithContext(context)
                .WithColor(color)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, Object context, Color32 color, FontStyle fontStyle, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithContext(context)
                .WithColor(color)
                .WithFontStyle(fontStyle)
                .WithCondition(condition)
                .Log();

        // ---------- С тегом ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, TagType tag, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithTag(tag)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, TagType tag, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithTag(tag)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, TagType tag, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithTag(tag)
                .WithCondition(condition)
                .Log();

        // ---------- С тегом и контекстом ----------

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(object message, TagType tag, Object context, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithTag(tag)
                .WithContext(context)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogWarning(object message, TagType tag, Object context, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogWarning)
                .WithTag(tag)
                .WithContext(context)
                .WithCondition(condition)
                .Log();

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void LogError(object message, TagType tag, Object context, bool condition = true) =>
            LoggerBuilder.Message(message)
                .WithLogType(XLogType.LogError)
                .WithTag(tag)
                .WithContext(context)
                .WithCondition(condition)
                .Log();

        public static MessageInfo Message(object message) =>
            LoggerBuilder.Message(message);

        [HideInCallstack]
        [System.Diagnostics.Conditional(EditorCondition)]
        [System.Diagnostics.Conditional(DevelopmentCondition)]
        public static void Log(this MessageInfo messageInfo)
        {
            if (messageInfo.Condition == false)
                return;

            if (IsTagDisabled(messageInfo.Tag))
                return;

            string finalMessage = BuildFinalMessage(messageInfo);
            WriteToConsole(messageInfo, finalMessage);
        }

        // ----- private helpers -----

        [HideInCallstack]
        private static bool IsTagDisabled(TagType tag)
        {
            LoggerSettings settings = SettingsProvider.Settings;

            TagParams tagParams = tag == TagType.LOGGER
                ? settings.DefaultTag
                : (settings.TryGetTag(tag, out TagParams found) ? found : null);

            return tagParams != null && tagParams.IsOn == false;
        }

        [HideInCallstack]
        private static string BuildFinalMessage(MessageInfo info)
        {
            LoggerSettings settings = SettingsProvider.Settings;

            string tagString = string.Empty;

            if (settings.ShowTag)
            {
                tagString = info.Tag == TagType.LOGGER
                    ? LoggerTagFormatter.GetDefaultTagFormatted()
                    : LoggerTagFormatter.GetTagFormatted(info.Tag);
            }

            string formattedMessage = TextFormatter.FormatMessage(
                info.Message,
                info.MessageColor,
                info.FontStyle);

            return string.IsNullOrEmpty(tagString)
                ? formattedMessage
                : $"{tagString} {formattedMessage}";
        }

        [HideInCallstack]
        private static void WriteToConsole(MessageInfo info, string message)
        {
            switch (info.MessageType)
            {
                case XLogType.Log:
                    if (info.Context != null)
                        Debug.Log(message, info.Context);
                    else
                        Debug.Log(message);
                    break;

                case XLogType.LogWarning:
                    if (info.Context != null)
                        Debug.LogWarning(message, info.Context);
                    else
                        Debug.LogWarning(message);
                    break;

                case XLogType.LogError:
                    if (info.Context != null)
                        Debug.LogError(message, info.Context);
                    else
                        Debug.LogError(message);
                    break;
            }
        }
    }
}