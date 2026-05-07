using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Gameplay.MiniMap
{
    public class IconMapObject : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _rectTransform;

        public void SetAnchoredPosition(Vector2 position) =>
            _rectTransform.anchoredPosition = position;

        public void SetColor(Color color) =>
            _image.color = color;

        public void SetSize(Vector2 size) =>
            _rectTransform.sizeDelta = size;

        public void SetRotation(float rotation) =>
            _rectTransform.rotation = Quaternion.Euler(0, 0, -rotation);
    }
}