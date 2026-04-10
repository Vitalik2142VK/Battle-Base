using System;
using TMPro;
using UnityEngine;

namespace BattleBase.Services.Localization
{
    [Serializable]
    public class TextLangParams
    {
        [SerializeField][TextArea] private string _text = "text";
        [SerializeField] private TMP_FontAsset _font;
        [SerializeField] private Material _preset;

        public string Text => _text;

        public TMP_FontAsset Font => _font;

        public Material Preset => _preset;
    }
}