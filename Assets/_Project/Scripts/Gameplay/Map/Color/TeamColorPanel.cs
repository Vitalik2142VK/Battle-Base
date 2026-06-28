using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    public class TeamColorPanel : MonoBehaviour
    {
        private readonly List<ColorCell> _cells = new();

        [SerializeField] private ColorCell _prefab;
        [SerializeField] private Transform _context;

        public event Action<int> Clicked;

        private void OnEnable()
        {
            foreach (ColorCell box in _cells)
                box.Clicked += OnColorBoxClick;
        }

        private void OnDisable() =>
            Unsubscribe();

        public void Init(IReadOnlyList<Color> colors)
        {
            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            ClearContext();

            for (int i = 0; i < colors.Count; i++)
            {
                ColorCell cell = Instantiate(_prefab, _context);
                cell.Init(colors[i], i);
                _cells.Add(cell);
            }
        }

        public void EnableInteractableAll()
        {
            foreach (ColorCell box in _cells)
                box.EnableInteractable();
        }

        public void DisableInteractable(int index) =>
            _cells[index].DisableInteractable();

        public void SelectOnly(int cellIndex)
        {
            foreach (ColorCell cell in _cells)
                cell.Deselect();

            _cells[cellIndex].Select();
        }

        private void Unsubscribe()
        {
            foreach (ColorCell box in _cells)
            {
                if (box != null)
                    box.Clicked -= OnColorBoxClick;
            }
        }

        private void ClearContext()
        {
            Unsubscribe();

            foreach (Transform child in _context)
                Destroy(child.gameObject);

            _cells.Clear();
        }

        private void OnColorBoxClick(ColorCell currentCell) =>
            Clicked?.Invoke(currentCell.Index);
    }
}