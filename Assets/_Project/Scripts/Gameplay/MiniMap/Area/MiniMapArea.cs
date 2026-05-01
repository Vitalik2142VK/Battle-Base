using System;
using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _area;
        [SerializeField] private ScreenOrientation _orientation;

        public event Action SizeChanged;

        public Rect Rect => _area.rect;

        public ScreenOrientation Orientation => _orientation;

        public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
        {
            _area.SetSizeWithCurrentAnchors(axis, size);
            SizeChanged?.Invoke();
        }
    }
}