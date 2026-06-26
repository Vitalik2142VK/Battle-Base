using System;
using UnityEngine;

namespace BattleBase.Localization
{
    [Serializable]
    public class TextLangParams : ITextLangParams
    {
        [SerializeField][TextArea] private string _text = "text";

        public TextLangParams(ITextLangParams other)
        {
            _text = other.Text;
        }

        public string Text => _text;
    }
}