using System;
using BattleBase.Static;
using YG;

namespace BattleBase.Services.Localization
{
    public static class YandexGameLanguageSystemAdapter
    {
        static YandexGameLanguageSystemAdapter()
        {
            YG2.onSwitchLang += OnSwitchLang;

            SetLanguage(YG2.lang);
        }

        public static event Action Changed;

        public static ILanguage CurrentLanguage { get; private set; }

        public static void SwitchLanguage(ILanguage language)
        {
            string langCode = language switch
            {
                RuLanguage => Constants.LangRu,
                EnLanguage => Constants.LangEn,
                TrLanguage => Constants.LangTr,
                _ => Constants.LangEn
            };

            YG2.SwitchLanguage(langCode);
        }

        public static void Previous()
        {
            ILanguage prev = CurrentLanguage switch
            {
                RuLanguage => TrLanguage.Instance,
                EnLanguage => RuLanguage.Instance,
                TrLanguage => EnLanguage.Instance,
                _ => EnLanguage.Instance
            };

            SwitchLanguage(prev);
        }

        public static void Next()
        {
            ILanguage next = CurrentLanguage switch
            {
                RuLanguage => EnLanguage.Instance,
                EnLanguage => TrLanguage.Instance,
                TrLanguage => RuLanguage.Instance,
                _ => EnLanguage.Instance
            };

            SwitchLanguage(next);
        }

        private static void SetLanguage(string langCode)
        {
            CurrentLanguage = langCode switch
            {
                Constants.LangRu => RuLanguage.Instance,
                Constants.LangEn => EnLanguage.Instance,
                Constants.LangTr => TrLanguage.Instance,
                _ => EnLanguage.Instance,
            };

            Changed?.Invoke();
        }

        private static void OnSwitchLang(string lang) =>
            SetLanguage(lang);
    }
}