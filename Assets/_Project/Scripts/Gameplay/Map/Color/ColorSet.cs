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

        private void Awake()
        {
            ClearContext();

            foreach (Color color in _config.Colors)
            {
                ColorBox box = Instantiate(_prefab, _context);
                box.SetColor(color);
                _boxes.Add(box);
            }
        }

        private void ClearContext()
        {
            foreach(Transform child in _context)
                Destroy(child.gameObject);
        }
    }
}