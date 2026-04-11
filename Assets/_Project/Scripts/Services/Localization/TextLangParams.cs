using System;
using TMPro;
using UnityEngine;

namespace BattleBase.Services.Localization
{
    [Serializable]
    public class TextLangParams
    {
        [SerializeField][TextArea] private string _text = "text";

        public string Text => _text;
    }
}