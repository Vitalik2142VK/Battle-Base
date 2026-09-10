namespace FiXiK.CustomLogger.Editor
{
    public interface ILanguage
    {
        public string Language { get; }

        public string LanguageName { get; }

        public string SettingsWindowTitle { get; }

        public string TagSettingsHeader { get; }

        public string MessageSettingsHeader { get; }

        public string ColorSettingsHeader { get; }

        public string SaveButtonLabel { get; }

        public string ResetButtonLabel { get; }

        public string TestButtonLabel { get; }

        public string CreateAssetButtonLabel { get; }

        public string SettingsNotFoundMessage { get; }

        public string SettingsSavedMessage { get; }

        public string SettingsResetMessage { get; }

        public string FailedToLoadSettings { get; }

        public string LogLabel { get; }

        public string WarningLabel { get; }

        public string ErrorLabel { get; }

        public string ColorLabel { get; }

        public string FontStyleLabel { get; }

        public string ShowTagLabel { get; }

        public string DefaultTagLabel { get; }

        public string TagsLabel { get; }

        public string ColorsLabel { get; }

        public string TestTitle { get; }

        public string StandardLogsTitle { get; }

        public string TagsTitle { get; }

        public string ColorsTitle { get; }

        public string TestNormal { get; }

        public string TestWarning { get; }

        public string TestError { get; }

        public string TestComplete { get; }

        public string AboutTitle { get; }

        public string AboutMessage { get; }

        public string AboutButtonOk { get; }

        public string AboutButtonTelegram { get; }

        public string WelcomeTitle { get; }

        public string WelcomeMessage { get; }

        public string WelcomeButtonOk { get; }

        public string MenuAboutItem { get; }

        public string MenuWelcomeItem { get; }
    }
}