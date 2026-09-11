using System;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    [Serializable]
    public class MessageParams
    {
        [SerializeField] private Color32 _textColor;
        [SerializeField] private FontStyle _fontStyle;

        public MessageParams() { }

        public MessageParams(Color32 textColor, FontStyle fontStyle)
        {
            _textColor = textColor;
            _fontStyle = fontStyle;
        }

        public Color32 Color => _textColor;

        public FontStyle FontStyle => _fontStyle;
    }
}