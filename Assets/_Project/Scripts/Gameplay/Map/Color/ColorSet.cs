using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class ColorSet : MonoBehaviour
    {
        [SerializeField] private ColorSetConfig _config;
        [SerializeField] private ColorBox _prefab;
        [SerializeField] private Transform _context;

        private readonly List<ColorBox> _boxes = new();

        public event Action<int> Clicked;

        public Color CurrentColor { get; private set; }

        public int CurrentColorIndex { get; private set; }

        private void OnEnable()
        {
            foreach (ColorBox box in _boxes)
                box.Clicked += OnColorBoxClick;
        }

        private void OnDisable()
        {
            foreach (ColorBox box in _boxes)
                box.Clicked -= OnColorBoxClick;
        }

        public void Init(int indexColor)
        {
            CurrentColor = _config.Colors[indexColor];
            CurrentColorIndex = indexColor;
            ClearContext();

            foreach (Color color in _config.Colors)
            {
                ColorBox box = Instantiate(_prefab, _context);
                box.Init(color);
                box.Deselect();
                _boxes.Add(box);
            }

            _boxes[indexColor].Select();
        }

        public void EnableInteractableAll()
        {
            foreach (ColorBox box in _boxes)
                box.EnableInteractable();
        }

        public void DisableInteractable(int index) =>
            _boxes[index].DisableInteractable();

        private void ClearContext()
        {
            foreach(Transform child in _context)
                Destroy(child.gameObject);
        }

        private void OnColorBoxClick(ColorBox colorBox)
        {
            foreach (ColorBox box in _boxes)
                box.Deselect();

            colorBox.Select();
            CurrentColor = colorBox.Color;
            CurrentColorIndex = _boxes.IndexOf(colorBox);

            Clicked?.Invoke(_boxes.IndexOf(colorBox));
        }
    }
}