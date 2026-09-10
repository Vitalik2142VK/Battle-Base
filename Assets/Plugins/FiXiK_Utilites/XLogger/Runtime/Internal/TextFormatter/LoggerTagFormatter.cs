namespace FiXiK.CustomLogger
{
    public static class LoggerTagFormatter
    {
        public static string GetDefaultTagFormatted()
        {
            LoggerSettings settings = SettingsProvider.Settings;

            if (settings.ShowTag == false)
                return string.Empty;

            TagParams defaultTag = settings.DefaultTag;

            return TextFormatter
                .WrapSquareBrackets(defaultTag.Name)
                .WithColor(defaultTag.Color)
                .WithStyle(LoggerConstants.DefaultTagStyle);
        }

        public static string GetTagFormatted(TagType tagType)
        {
            LoggerSettings settings = SettingsProvider.Settings;

            if (settings.ShowTag == false)
                return string.Empty;

            if (settings.TryGetTag(tagType, out TagParams tagParams) == false)
                return string.Empty;

            return TextFormatter
                .WrapSquareBrackets(tagParams.Name)
                .WithColor(tagParams.Color)
                .WithStyle(LoggerConstants.DefaultTagStyle);
        }
    }
}