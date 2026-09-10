using System;
using UnityEditor;

namespace FiXiK.CustomLogger.Editor
{
    public static class LanguageManager
    {
        private static ILanguage _currentLanguage;

        private static readonly ILanguage[] AvailableLanguages = new ILanguage[]
        {
            new English(),
            new Russian()
        };

        public static event Action LanguageChanged;

        public static ILanguage Current
        {
            get
            {
                _currentLanguage ??= AvailableLanguages[0];

                return _currentLanguage;
            }
            set
            {
                if (_currentLanguage != value)
                {
                    _currentLanguage = value;
                    LanguageChanged?.Invoke();
                }
            }
        }

        public static ILanguage[] GetAllLanguages() =>
            AvailableLanguages;

        public static string GetLanguageName(ILanguage language) =>
            language.LanguageName;

        public static void LoadFromPrefs()
        {
            string savedLanguageKey = EditorPrefs.GetString(LoggerEditorConstants.EditorPrefsKey, new English().LanguageName);

            foreach (ILanguage language in AvailableLanguages)
            {
                if (language.LanguageName == savedLanguageKey)
                {
                    _currentLanguage = language;

                    return;
                }
            }

            _currentLanguage = AvailableLanguages[0];
        }

        public static void SaveToPrefs()
        {
            if (_currentLanguage != null)
                EditorPrefs.SetString(LoggerEditorConstants.EditorPrefsKey, _currentLanguage.LanguageName);
            else
                EditorPrefs.SetString(LoggerEditorConstants.EditorPrefsKey, new English().LanguageName);
        }
    }
}