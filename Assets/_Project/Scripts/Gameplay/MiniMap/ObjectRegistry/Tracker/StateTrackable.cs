using UnityEngine;

namespace BattleBase.Gameplay.MiniMap
{
    public class StateTrackable : Trackable
    {
        [SerializeField] private Color _color;

        private void Awake() =>
            SetColor(_color);
    }
}