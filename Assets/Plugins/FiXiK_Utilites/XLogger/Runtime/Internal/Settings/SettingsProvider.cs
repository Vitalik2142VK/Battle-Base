namespace FiXiK.CustomLogger
{
    public static class SettingsProvider
    {
        private static LoggerSettings _settings;

        public static LoggerSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = SettingsLoader.Load();

                return _settings;
            }
        }
    }
}