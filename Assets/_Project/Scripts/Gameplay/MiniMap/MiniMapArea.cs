using System;
using UnityEngine;
using static UnityEngine.RectTransform;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _area;

        public event Action SizeChanged;

        public Rect Rect => _area.rect;

        public void SetSizeWithCurrentAnchors(Axis axis, float size)
        {
            _area.SetSizeWithCurrentAnchors(axis, size);
            SizeChanged?.Invoke();
        }
    }
}