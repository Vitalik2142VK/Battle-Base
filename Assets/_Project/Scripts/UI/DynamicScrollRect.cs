using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.UI
{
    [ExecuteAlways]
    public class DynamicScrollRect : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform _viewport;
        [SerializeField] private RectTransform _content;
        [SerializeField] private GridLayoutGroup _grid;

        [Header("Settings")]
        [SerializeField][Min(1)] private int _maxVisibleColumns = 3;

        private int _currentVisibleColumns = 3;

        private void Awake()
        {
            _currentVisibleColumns = _maxVisibleColumns;
        }

        [ContextMenu("Refresh")]
        public void Refresh()
        {
            if (_viewport == null || _content == null || _grid == null)
                return;

            int childCount = 0;

            foreach (Transform child in _content)
            {
                if (child.gameObject.activeSelf)
                    childCount++;
            }

            int rows = _grid.constraint switch
            {
                GridLayoutGroup.Constraint.FixedRowCount =>
                    Mathf.Max(1, _grid.constraintCount),

                GridLayoutGroup.Constraint.FixedColumnCount =>
                    Mathf.Max(1, Mathf.CeilToInt((float)childCount / Mathf.Max(1, _grid.constraintCount))),

                _ => Mathf.Max(1, Mathf.FloorToInt(
                    (_content.rect.height
                    - _grid.padding.top
                    - _grid.padding.bottom
                    + _grid.spacing.y)
                    / (_grid.cellSize.y + _grid.spacing.y)))
            };

            int columns = childCount == 0 ? 0 : Mathf.CeilToInt((float)childCount / rows);

            float requiredWidth = childCount == 0
                ? _grid.padding.left + _grid.padding.right
                : _grid.padding.left
                  + _grid.padding.right
                  + columns * _grid.cellSize.x
                  + (columns - 1) * _grid.spacing.x;

            int visibleColumns = Mathf.Min(columns, _currentVisibleColumns);

            float visibleWidth = visibleColumns == 0
                ? _grid.padding.left + _grid.padding.right
                : _grid.padding.left
                  + _grid.padding.right
                  + visibleColumns * _grid.cellSize.x
                  + (visibleColumns - 1) * _grid.spacing.x;

            Vector2 viewportSize = _viewport.sizeDelta;
            viewportSize.x = visibleWidth;
            _viewport.sizeDelta = viewportSize;

            Vector2 contentSize = _content.sizeDelta;
            contentSize.x = requiredWidth;
            _content.sizeDelta = contentSize;
        }

        public void SetMaxVisibleColumns(int columns)
        {
            if (columns <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(columns));

            _currentVisibleColumns = columns;
        }
    }
}