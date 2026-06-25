using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.ShopSystem
{
    public class BackgroundFlicker : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private float _minimumAlpha;
        [SerializeField] private float _maximumAlpha;
        [SerializeField] private float _speed;

        private Color _color;

        private void Awake() =>
            _color = _image.color;

        private void Update()
        {
            float t = Mathf.PingPong(Time.time * _speed, 1f);
            _color.a = Mathf.Lerp(_minimumAlpha, _maximumAlpha, t);
            _image.color = _color;
        }
    }
}