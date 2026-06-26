using BattleBase.Gameplay.MiniMap;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Colored
{
    public partial class ColoredActorView : MonoBehaviour, IColoredActorView
    {
        [SerializeField] private Trackable _trackable;
        [SerializeField] private MaterialColorChanger _colorChanger;

        private IColored _colored;
        
        public ITrackable Trackable => _trackable;

        private void OnEnable()
        {
            if (_colored != null)
                _colored.ColorChanged += ChangeColor;
        }

        private void OnDisable()
        {
            if (_colored != null)
                _colored.ColorChanged -= ChangeColor;

            _trackable.Deactivate();
        }

        public void Init(IColored colored)
        {
            _colored ??= colored ?? throw new System.ArgumentNullException(nameof(colored));

            if (gameObject.activeSelf)
                colored.ColorChanged += ChangeColor;
        }

        private void ChangeColor(Color color)
        {
            if (_colorChanger.CurrentColor != color)
                _colorChanger.Change(color);

            if (_trackable.Color != color)
                _trackable.SetColor(color);
        }
    }
}
