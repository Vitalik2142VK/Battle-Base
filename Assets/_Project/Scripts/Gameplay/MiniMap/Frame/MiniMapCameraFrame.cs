using UnityEngine;
using static UnityEngine.RectTransform;

namespace BattleBase.Gameplay.MiniMap
{
    public class MiniMapCameraFrame : MonoBehaviour
    {
        [SerializeField] private RectTransform _frame;

        public void SetSizeWithCurrentAnchors(Axis axis, float size) =>
            _frame.SetSizeWithCurrentAnchors(axis, size);

        public void SetAnchoredPosition(Vector2 position) =>
            _frame.anchoredPosition = position;
    }
}