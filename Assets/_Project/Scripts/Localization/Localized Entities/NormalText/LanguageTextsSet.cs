using System;
using UnityEngine;

namespace BattleBase.Localization
{
    [Serializable]
    public class LanguageTextsSet : ILanguageVisitor, ILanguageTextsSet
    {
        [SerializeField] private TextLangParams _ru;
        [SerializeField] private TextLangParams _en;
        [SerializeField] private TextLangParams _tr;

        private TextLangParams _result;

        public LanguageTextsSet(ILanguageTextsSet languageTextsSet)
        {
            if (languageTextsSet == null)
                throw new ArgumentNullException(nameof(languageTextsSet));

            _ru = new(languageTextsSet.Ru);
            _en = new(languageTextsSet.En);
            _tr = new(languageTextsSet.Tr);
        }

        public ITextLangParams Ru => _ru;

        public ITextLangParams En => _en;

        public ITextLangParams Tr => _tr;

        public TextLangParams GetByLanguage(ILanguage language)
        {
            language.Accept(this);

            return _result;
        }

        void ILanguageVisitor.Visit(RuLanguage lang) => _result = _ru;

        void ILanguageVisitor.Visit(EnLanguage lang) => _result = _en;

        void ILanguageVisitor.Visit(TrLanguage lang) => _result = _tr;
    }
}