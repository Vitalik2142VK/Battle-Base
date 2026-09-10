using System;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    [Serializable]
    public class TagParams
    {
        [SerializeField] private string _name;
        [SerializeField] private Color32 _color;
        [SerializeField] private bool _isOn = true;

        public TagParams() { }

        public TagParams(string name, Color32 color)
        {
            _name = name;
            _color = color;
        }

        public string Name => _name;

        public Color32 Color => _color;

        public bool IsOn => _isOn;
    }
}