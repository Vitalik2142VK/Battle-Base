using System;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    [Serializable]
    public class ColorParams
    {
        [SerializeField] private string _name;
        [SerializeField] private Color32 _color;

        public ColorParams() { }

        public ColorParams(string name, Color32 color)
        {
            _name = name;
            _color = color;
        }

        public string Name => _name;

        public Color32 Color => _color;
    }
}