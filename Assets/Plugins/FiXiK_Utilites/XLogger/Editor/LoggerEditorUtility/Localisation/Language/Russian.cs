namespace FiXiK.CustomLogger.Editor
{
    public class Russian : ILanguage
    {
        public string Language => "Язык";

        public string LanguageName => "Русский";

        public string SettingsWindowTitle => "Настройки логгера";

        public string TagSettingsHeader => "Настройки тегов";

        public string ColorSettingsHeader => "Настройки цветов";

        public string MessageSettingsHeader => "Настройки сообщений";

        public string SaveButtonLabel => "Сохранить";

        public string ResetButtonLabel => "Сбросить до заводских настроек";

        public string TestButtonLabel => "Тест логов";

        public string CreateAssetButtonLabel => "Создать файл настроек";

        public string SettingsNotFoundMessage => "Файл LoggerSettings не найден. Пожалуйста, создайте его.";

        public string SettingsSavedMessage => "Настройки логгера успешно сохранены.";

        public string SettingsResetMessage => "Настройки логгера сброшены к стандартным.";

        public string FailedToLoadSettings => "Не удалось загрузить настройки.";

        public string LogLabel => "Обычный лог";

        public string WarningLabel => "Лог с предупреждением";

        public string ErrorLabel => "Лог с ошибкой";

        public string ColorLabel => "Цвет";

        public string FontStyleLabel => "Стиль шрифта";

        public string ShowTagLabel => "Показывать тег";

        public string DefaultTagLabel => "Тег по умолчанию";

        public string TagsLabel => "Теги";

        public string ColorsLabel => "Цвета";

        public string TestTitle => "=== ТЕСТ ЛОГОВ ===";

        public string StandardLogsTitle => "--- Стандартные логи ---";

        public string TagsTitle => "--- Теги ---";

        public string ColorsTitle => "--- Цвета ---";

        public string TestNormal => "Обычный лог";

        public string TestWarning => "Предупреждение";

        public string TestError => "Ошибка";

        public string TestComplete => "=== ТЕСТ ЗАВЕРШЁН ===";

        public string AboutTitle => "О логгере";
        public string AboutMessage =>
            "Custom Logger\n" +
            $"<color=yellow>Версия: {LoggerConstants.Version}</color>\n\n" +
            "Гибкая система логирования с тегами, цветами и настраиваемыми параметрами.\n\n" +
            "С уважением, ваш FiXiK";

        public string AboutButtonOk => "Ок";

        public string AboutButtonTelegram => "Мой телеграм";

        public string WelcomeTitle => "Logger";

        public string WelcomeMessage =>
            "<color=green>Пакет успешно установлен!</color>\n\n" +
            "Окно настройки логгера открыто автоматически.\n" +
            $"Вы всегда можете открыть его через меню:\n" +
            $"\"{LoggerEditorConstants.SettingsMenuPath}\"\n\n" +
            "С уважением, ваш FiXiK";

        public string WelcomeButtonOk => "От души, бро!";

        public string MenuAboutItem => "О программе";

        public string MenuWelcomeItem => "Показать приветственный диалог";
    }
}