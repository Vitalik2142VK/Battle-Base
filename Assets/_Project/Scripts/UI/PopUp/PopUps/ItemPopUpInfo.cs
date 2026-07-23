using System;
using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public readonly struct ItemPopUpInfo
    {
        private readonly Sprite _preview;
        private readonly ILanguageTextsSet _name;
        private readonly ILanguageTextsSet _description;

        public ItemPopUpInfo(Sprite preview, ILanguageTextsSet name, ILanguageTextsSet description)
        {
            _name = new LanguageTextsSet(name) ?? throw new ArgumentNullException(nameof(name));
            _description = new LanguageTextsSet(description) ?? throw new ArgumentNullException(nameof(description));
            _preview = preview;
        }

        public Sprite Preview => _preview;

        public ILanguageTextsSet Name => _name;

        public ILanguageTextsSet Description => _description;
    }
}