using System;
using BattleBase.Gameplay.CameraNavigation;
using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _area;
        [SerializeField] private ScreenOrientationType _orientation;

        public event Action SizeChanged;

        public Rect Rect => _area.rect;

        public ScreenOrientationType Orientation => _orientation;

        public void SetSizeWithCurrentAnchors(RectTransform.Axis axis, float size)
        {
            _area.SetSizeWithCurrentAnchors(axis, size);
            SizeChanged?.Invoke();
        }
    }
}