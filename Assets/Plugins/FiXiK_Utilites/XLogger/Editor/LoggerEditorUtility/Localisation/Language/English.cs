namespace FiXiK.CustomLogger.Editor
{
    public class English : ILanguage
    {
        public string Language => "Language";

        public string LanguageName => "English";

        public string SettingsWindowTitle => "Logger Settings";

        public string TagSettingsHeader => "Tag Settings";

        public string MessageSettingsHeader => "Message Settings";

        public string ColorSettingsHeader => "Color Settings";

        public string SaveButtonLabel => "Save Settings";

        public string ResetButtonLabel => "Reset to Default";

        public string TestButtonLabel => "Test Logs";

        public string CreateAssetButtonLabel => "Create Settings Asset";

        public string SettingsNotFoundMessage => "LoggerSettings asset not found. Please create it.";

        public string SettingsSavedMessage => "Logger settings saved successfully.";

        public string SettingsResetMessage => "Logger settings reset to default.";

        public string FailedToLoadSettings => "Failed to load settings.";

        public string LogLabel => "Log";

        public string WarningLabel => "Warning";

        public string ErrorLabel => "Error";

        public string ColorLabel => "Color";

        public string FontStyleLabel => "Font Style";

        public string ShowTagLabel => "Show Tag";

        public string DefaultTagLabel => "Default Tag";

        public string TagsLabel => "Tags";

        public string ColorsLabel => "Colors";

        public string TestTitle => "=== LOGGER INITIAL LOGS TEST ===";

        public string StandardLogsTitle => "--- Standard Logs ---";

        public string TagsTitle => "--- Tags ---";

        public string ColorsTitle => "--- Colors ---";

        public string TestNormal => "Normal log";

        public string TestWarning => "Warning log";

        public string TestError => "Error log";

        public string TestComplete => "=== TEST COMPLETE ===";

        public string AboutTitle => "About Logger";
        public string AboutMessage =>
            "Custom Logger\n" +
            $"<color=yellow>Version: {LoggerConstants.Version}</color>\n\n" +
            "A flexible logging system with tags, colors, and custom settings.\n\n" +
            "Best regards, your FiXiK";

        public string AboutButtonOk => "OK";

        public string AboutButtonTelegram => "My Telegram";

        public string WelcomeTitle => "Logger";

        public string WelcomeMessage =>
            "<color=green>Package successfully installed!</color>\n\n" +
            "Logger settings window opened automatically.\n" +
            $"You can always open it via menu:\n" +
            $"\"{LoggerEditorConstants.SettingsMenuPath}\"\n\n" +
            "Best regards, your FiXiK";

        public string WelcomeButtonOk => "Awesome, thanks!";

        public string MenuAboutItem => "About";

        public string MenuWelcomeItem => "Show Welcome Dialog";
    }
}