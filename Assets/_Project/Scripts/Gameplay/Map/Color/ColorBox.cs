using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Gameplay.Map
{
    public class ColorBox : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetColor(Color color) =>
            _image.color = color;
    }
}