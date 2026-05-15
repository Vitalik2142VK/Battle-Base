using UnityEngine;

namespace BattleBase.Gameplay
{
    public abstract class Building : Entity
    {
        [SerializeField] private Color _color;

        private void Awake() =>
            SetColor(_color);
    }
}